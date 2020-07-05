using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;

namespace KnstArchitecture.UnitOfWorks
{
    public interface ISqliteUnitOfWork : IEFCoreUnitOfWork
    {
        new ISqliteDbSession CreateDbSession();
        new ISqliteDbSession GetDefaultDbSession();
        new TRepo Use<TRepo>() where TRepo : IEFCoreRepo;
        TRepo Use<TRepo>(ISqliteDbSession dbSession) where TRepo : IEFCoreRepo;
    }
}