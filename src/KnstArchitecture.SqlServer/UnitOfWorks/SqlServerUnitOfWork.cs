using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.UnitOfWorks
{
    public class SqlServerUnitOfWork : EFCoreUnitOfWork, ISqlServerUnitOfWork
    {
        public SqlServerUnitOfWork(IServiceProvider serviceProvider, ISqlServerDbSession sqlServerDbSession) : base(serviceProvider, sqlServerDbSession) { }

        TRepo ISqlServerUnitOfWork.Use<TRepo>() => this.Use<TRepo>(this.GetDefaultDbSession());

        public TRepo Use<TRepo>(ISqlServerDbSession dbSession) where TRepo : IEFCoreRepo => base.Create<TRepo>(dbSession);

        public new ISqlServerDbSession CreateDbSession() => _serviceProvider.GetRequiredService<ISqlServerDbSession>();

        public new ISqlServerDbSession GetDefaultDbSession() => base.GetDefaultDbSession() as ISqlServerDbSession;
    }
}