using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.UnitOfWorks
{
    public class SqliteUnitOfWork : EFCoreUnitOfWork, ISqliteUnitOfWork
    {
        public SqliteUnitOfWork(IServiceProvider serviceProvider, ISqliteDbSession sqliteDbSession) : base(serviceProvider, sqliteDbSession) { }

        TRepo ISqliteUnitOfWork.Use<TRepo>() => this.Use<TRepo>(this.GetDefaultDbSession());

        public TRepo Use<TRepo>(ISqliteDbSession dbSession) where TRepo : IEFCoreRepo => base.Create<TRepo>(dbSession);

        public new ISqliteDbSession CreateDbSession() => _serviceProvider.GetRequiredService<ISqliteDbSession>();

        public new ISqliteDbSession GetDefaultDbSession() => base.GetDefaultDbSession() as ISqliteDbSession;
    }
}