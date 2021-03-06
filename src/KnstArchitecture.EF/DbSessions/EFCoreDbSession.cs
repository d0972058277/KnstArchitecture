using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using KnstArchitecture.EF;
using KnstArchitecture.EF.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.DbSessions
{
    public abstract class EFCoreDbSession : SqlDbSession, IEFCoreDbSession
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope _serviceScope;
        private HashSet<KnstDbContext> _dbContexts;

        public IEnumerable<KnstDbContext> Observers { get => _dbContexts.ToList().AsReadOnly(); }

        protected EFCoreDbSession(IDbSessionBag dbSessionBag, IDbConnection connection, IServiceProvider serviceProvider) : base(dbSessionBag, connection)
        {
            _serviceProvider = serviceProvider;
            _serviceScope = _serviceProvider.CreateScope();
            _dbContexts = new HashSet<KnstDbContext>();
        }

        private void DisposeDbContexts()
        {
            foreach (var dbContext in _dbContexts)
            {
                dbContext.Dispose();
            }
            _dbContexts.Clear();
        }

        public override IDbSession BeginTransaction()
        {
            base.BeginTransaction();
            NotifyObserversUseTransaction();
            return this;
        }

        public override void Commit()
        {
            base.Commit();
            NotifyObserversUseTransaction();
        }

        public virtual TContext GetCtx<TContext>() where TContext : KnstDbContext
        {
            TContext dbContext = _dbContexts.Where(obs => obs is TContext).SingleOrDefault() as TContext;

            if (dbContext is null)
            {
                dbContext = _serviceScope.ServiceProvider.GetRequiredService<TContext>();

                // dispose 舊的 connection
                var abandonedDbSession = dbContext.DbSession;
                abandonedDbSession.GetConnection()?.Dispose();

                dbContext.DbSession = this;
                _dbContexts.Add(dbContext);
            }

            if (IsTransaction && dbContext.Database.CurrentTransaction is null)
            {
                dbContext.Database.UseTransaction(_transaction as DbTransaction);
            }

            return dbContext;
        }

        public override void Rollback()
        {
            base.Rollback();
            NotifyObserversUseTransaction();
        }

        public void SaveChanges()
        {
            foreach (var dbContext in _dbContexts)
            {
                dbContext.SaveChanges();
            }
        }

        public async Task SaveChangesAsync()
        {
            foreach (var dbContext in _dbContexts)
            {
                await dbContext.SaveChangesAsync();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            // 釋放非託管資源
            DisposeDbContexts();
            _serviceScope.Dispose();

            // 釋放託管資源
            if (disposing)
            {
                _dbContexts = null;
            }

            base.Dispose(disposing);
        }

        IEFCoreDbSession IEFCoreDbSession.BeginTransaction() => this.BeginTransaction() as IEFCoreDbSession;

        public void NotifyObserversUseTransaction()
        {
            foreach (var observer in _dbContexts)
            {
                (observer as ITransactionObserver).UseTransaction(_transaction);
            }
        }

        public void Attach(KnstDbContext observer)
        {
            _dbContexts.Add(observer);
        }

        public void Detach(KnstDbContext observer)
        {
            _dbContexts.Remove(observer);
        }
    }
}