using KnstArchitecture.DbSessions;
using KnstArchitecture.EF.DbContexts;

namespace KnstArchitecture.Repos
{
    public interface IEFCoreRepo : ISqlRepo
    {
        new IEFCoreDbSession DbSession { get; }
    }

    public interface IEFCoreRepo<TContext> : IEFCoreRepo where TContext : KnstDbContext
    {
        TContext DbContext { get; }
    }
}