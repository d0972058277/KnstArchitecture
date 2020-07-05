using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;

namespace KnstArchitecture.UnitOfWorks
{
    public interface IMongoUnitOfWork : IUnitOfWork
    {
        new IMongoDbSession CreateDbSession();
        new IMongoDbSession GetDefaultDbSession();
        TRepo Use<TRepo>() where TRepo : IMongoRepo;
        TRepo Use<TRepo>(IMongoDbSession dbSession) where TRepo : IMongoRepo;
    }
}