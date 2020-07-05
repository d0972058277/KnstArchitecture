using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.UnitOfWorks
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private IDbSession _dbSession;
        protected IServiceProvider _serviceProvider;

        protected UnitOfWork(IServiceProvider serviceProvider, IDbSession dbSession)
        {
            _serviceProvider = serviceProvider;
            _dbSession = dbSession;
        }

        ~UnitOfWork() => Dispose(false);

        protected TRepo Create<TRepo>(IDbSession dbSession) where TRepo : IRepo
        {
            var factory = new Func<IServiceProvider, TRepo>(sp =>
                {
                    var repo = sp.GetRequiredService<TRepo>();
                    repo.DbSession = dbSession;
                    return repo;
                });

            var result = factory(_serviceProvider);
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            _serviceProvider = null;
            _dbSession.Dispose();
            _dbSession = null;

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract IDbSession CreateDbSession();

        public virtual void BeginTransaction() => _dbSession.BeginTransaction();

        public virtual void Commit() => _dbSession.Commit();

        public virtual void Rollback() => _dbSession.Rollback();

        public virtual void BeginTransaction(IDbSession dbSession) => dbSession.BeginTransaction();

        public virtual void Commit(IDbSession dbSession) => dbSession.Commit();

        public virtual void Rollback(IDbSession dbSession) => dbSession.Rollback();

        public IDbSession GetDefaultDbSession() => _dbSession;
    }
}