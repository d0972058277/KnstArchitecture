using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;

namespace KnstArchitecture.UnitOfWorks
{
    public interface IMySqlUnitOfWork : IEFCoreUnitOfWork
    {
        new IMySqlDbSession CreateDbSession();
        new IMySqlDbSession GetDefaultDbSession();
        new TRepo Use<TRepo>() where TRepo : IEFCoreRepo;
        TRepo Use<TRepo>(IMySqlDbSession dbSession) where TRepo : IEFCoreRepo;
    }
}