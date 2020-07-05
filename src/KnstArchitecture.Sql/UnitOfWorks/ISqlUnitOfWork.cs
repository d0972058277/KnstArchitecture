using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;

namespace KnstArchitecture.UnitOfWorks
{
    public interface ISqlUnitOfWork : IUnitOfWork
    {
        new ISqlDbSession CreateDbSession();
        new ISqlDbSession GetDefaultDbSession();
        TRepo Use<TRepo>() where TRepo : ISqlRepo;
        TRepo Use<TRepo>(ISqlDbSession dbSession) where TRepo : ISqlRepo;
    }
}