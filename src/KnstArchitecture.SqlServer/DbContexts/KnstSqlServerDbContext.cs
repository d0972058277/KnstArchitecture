using KnstArchitecture.UnitOfWorks;

namespace KnstArchitecture.EF.DbContexts
{
    public abstract class KnstSqlServerContext : KnstDbContext
    {
        protected KnstSqlServerContext(ISqlServerUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}