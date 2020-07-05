using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.UnitOfWorks
{
    public class MySqlUnitOfWork : EFCoreUnitOfWork, IMySqlUnitOfWork
    {
        public MySqlUnitOfWork(IServiceProvider serviceProvider, IMySqlDbSession mySqlDbSession) : base(serviceProvider, mySqlDbSession) { }

        public new IMySqlDbSession CreateDbSession() => _serviceProvider.GetRequiredService<IMySqlDbSession>();

        TRepo IMySqlUnitOfWork.Use<TRepo>() => this.Use<TRepo>(this.GetDefaultDbSession());

        public TRepo Use<TRepo>(IMySqlDbSession dbSession) where TRepo : IEFCoreRepo => base.Create<TRepo>(dbSession);
        public new IMySqlDbSession GetDefaultDbSession() => base.GetDefaultDbSession() as IMySqlDbSession;
    }
}