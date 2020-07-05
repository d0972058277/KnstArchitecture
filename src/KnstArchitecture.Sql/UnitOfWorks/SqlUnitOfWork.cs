using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.UnitOfWorks
{
    public class SqlUnitOfWork : UnitOfWork, ISqlUnitOfWork
    {
        public SqlUnitOfWork(IServiceProvider serviceProvider, ISqlDbSession sqlDbSession) : base(serviceProvider, sqlDbSession) { }

        public override IDbSession CreateDbSession() => (this as ISqlUnitOfWork).CreateDbSession();

        public TRepo Use<TRepo>() where TRepo : ISqlRepo => this.Use<TRepo>(this.GetDefaultDbSession());

        public TRepo Use<TRepo>(ISqlDbSession dbSession) where TRepo : ISqlRepo => base.Create<TRepo>(dbSession);

        ISqlDbSession ISqlUnitOfWork.CreateDbSession() => _serviceProvider.GetRequiredService<ISqlDbSession>();

        public new ISqlDbSession GetDefaultDbSession() => base.GetDefaultDbSession() as ISqlDbSession;
    }
}