using KnstArchitecture.UnitOfWorks;

namespace KnstArchitecture.EF.DbContexts
{
    public abstract class KnstSqliteContext : KnstDbContext
    {
        protected KnstSqliteContext(ISqliteUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}