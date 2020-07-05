using KnstArchitecture.DbSessions;
using KnstArchitecture.EF.DbContexts;
using KnstArchitecture.Repos;

namespace KnstArchitecture.UnitOfWorks
{
    public interface IEFCoreUnitOfWork : ISqlUnitOfWork
    {
        new IEFCoreDbSession CreateDbSession();
        new IEFCoreDbSession GetDefaultDbSession();
        new TRepo Use<TRepo>() where TRepo : IEFCoreRepo;
        TRepo Use<TRepo>(IEFCoreDbSession dbSession) where TRepo : IEFCoreRepo;
        void SaveChanges();
        void SaveChanges(IEFCoreDbSession dbSession);
        TContext GetCtx<TContext>() where TContext : KnstDbContext;
        TContext GetCtx<TContext>(IEFCoreDbSession dbSession) where TContext : KnstDbContext;
    }
}