using KnstArchitecture.UnitOfWorks;

namespace KnstArchitecture.EF.DbContexts
{
    public abstract class KnstMySqlDbContext : KnstDbContext
    {
        protected KnstMySqlDbContext(IMySqlUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}