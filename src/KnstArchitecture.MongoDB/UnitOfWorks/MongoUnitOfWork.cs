using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.UnitOfWorks
{
    public class MongoUnitOfWork : UnitOfWork, IMongoUnitOfWork
    {
        public MongoUnitOfWork(IServiceProvider serviceProvider, IMongoDbSession dbSession) : base(serviceProvider, dbSession) { }

        public override IDbSession CreateDbSession() => (this as IMongoUnitOfWork).CreateDbSession();

        IMongoDbSession IMongoUnitOfWork.CreateDbSession() => _serviceProvider.GetRequiredService<IMongoDbSession>();

        public TRepo Use<TRepo>() where TRepo : IMongoRepo => this.Use<TRepo>(this.GetDefaultDbSession());

        public TRepo Use<TRepo>(IMongoDbSession dbSession) where TRepo : IMongoRepo => base.Create<TRepo>(dbSession);

        public new IMongoDbSession GetDefaultDbSession() => base.GetDefaultDbSession() as IMongoDbSession;
    }
}