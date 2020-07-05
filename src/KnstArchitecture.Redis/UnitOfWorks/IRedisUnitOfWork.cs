using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;

namespace KnstArchitecture.UnitOfWorks
{
    public interface IRedisUnitOfWork : IUnitOfWork
    {
        new IRedisDbSession CreateDbSession();
        new IRedisDbSession GetDefaultDbSession();
        TRepo Use<TRepo>() where TRepo : IRedisRepo;
        TRepo Use<TRepo>(IRedisDbSession dbSession) where TRepo : IRedisRepo;
    }
}