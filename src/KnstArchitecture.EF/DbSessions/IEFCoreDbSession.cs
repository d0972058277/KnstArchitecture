using System.Threading.Tasks;
using KnstArchitecture.EF;
using KnstArchitecture.EF.DbContexts;

namespace KnstArchitecture.DbSessions
{
    public interface IEFCoreDbSession : ISqlDbSession, ITransactionSubject
    {
        new IEFCoreDbSession BeginTransaction();
        void SaveChanges();
        Task SaveChangesAsync();
        TContext GetCtx<TContext>() where TContext : KnstDbContext;
    }
}