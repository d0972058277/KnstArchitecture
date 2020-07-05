using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.UnitOfWorks
{
    public class RedisUnitOfWork : UnitOfWork, IRedisUnitOfWork
    {
        public RedisUnitOfWork(IServiceProvider serviceProvider, IDbSession dbSession) : base(serviceProvider, dbSession) { }

        public override IDbSession CreateDbSession() => (this as IRedisUnitOfWork).CreateDbSession();

        public TRepo Use<TRepo>() where TRepo : IRedisRepo => this.Use<TRepo>(this.GetDefaultDbSession());

        public TRepo Use<TRepo>(IRedisDbSession dbSession) where TRepo : IRedisRepo => Create<TRepo>(dbSession);

        IRedisDbSession IRedisUnitOfWork.CreateDbSession() => _serviceProvider.GetRequiredService<IRedisDbSession>();

        public new IRedisDbSession GetDefaultDbSession() => base.GetDefaultDbSession() as IRedisDbSession;
    }
}