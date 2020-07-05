using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.EF.DbContexts;
using KnstArchitecture.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.UnitOfWorks
{
    public abstract class EFCoreUnitOfWork : SqlUnitOfWork, IEFCoreUnitOfWork
    {
        protected EFCoreUnitOfWork(IServiceProvider serviceProvider, ISqlDbSession sqlDbSession) : base(serviceProvider, sqlDbSession) { }

        public override void BeginTransaction()
        {
            var dbSession = this.GetDefaultDbSession() as IEFCoreDbSession;
            dbSession.BeginTransaction();
        }

        public void SaveChanges() => (this.GetDefaultDbSession() as IEFCoreDbSession).SaveChanges();

        public void SaveChanges(IEFCoreDbSession dbSession) => dbSession.SaveChanges();

        TRepo IEFCoreUnitOfWork.Use<TRepo>() => this.Use<TRepo>(this.GetDefaultDbSession());

        public TRepo Use<TRepo>(IEFCoreDbSession dbSession) where TRepo : IEFCoreRepo => base.Create<TRepo>(dbSession);

        public new IEFCoreDbSession CreateDbSession() => _serviceProvider.GetRequiredService<IEFCoreDbSession>();

        public TContext GetCtx<TContext>() where TContext : KnstDbContext => _serviceProvider.GetRequiredService<TContext>();

        public TContext GetCtx<TContext>(IEFCoreDbSession dbSession) where TContext : KnstDbContext => dbSession.GetCtx<TContext>();

        public new IEFCoreDbSession GetDefaultDbSession() => base.GetDefaultDbSession() as IEFCoreDbSession;
    }
}