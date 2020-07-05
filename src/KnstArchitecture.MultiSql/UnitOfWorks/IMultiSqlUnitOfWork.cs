using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;

namespace KnstArchitecture.UnitOfWorks
{
    public interface IMultiSqlUnitOfWork : IUnitOfWork
    {
        new IMultiSqlDbSession CreateDbSession();
        new IMultiSqlDbSession GetDefaultDbSession();
        TRepo Use<TRepo>() where TRepo : IMultiSqlRepo;
        TRepo Use<TRepo>(ISqlDbSession dbSession) where TRepo : ISqlRepo;
        TRepo Use<TRepo>(IMultiSqlDbSession dbSession) where TRepo : IMultiSqlRepo;
    }
}