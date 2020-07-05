using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;

namespace KnstArchitecture.UnitOfWorks
{
    public interface ISqlServerUnitOfWork : IEFCoreUnitOfWork
    {
        new ISqlServerDbSession CreateDbSession();
        new ISqlServerDbSession GetDefaultDbSession();
        new TRepo Use<TRepo>() where TRepo : IEFCoreRepo;
        TRepo Use<TRepo>(ISqlServerDbSession dbSession) where TRepo : IEFCoreRepo;
    }
}