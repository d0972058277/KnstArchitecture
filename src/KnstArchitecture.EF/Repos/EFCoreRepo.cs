using KnstArchitecture.DbSessions;
using KnstArchitecture.EF.DbContexts;
using KnstArchitecture.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace KnstArchitecture.Repos
{
    public abstract class EFCoreRepo : SqlRepo, IEFCoreRepo
    {
        protected EFCoreRepo(ISqlUnitOfWork unitOfWork) : base(unitOfWork) { }

        public new IEFCoreDbSession DbSession => (this as IRepo).DbSession as IEFCoreDbSession;
    }

    public abstract class EFCoreRepo<TContext> : EFCoreRepo, IEFCoreRepo<TContext> where TContext : KnstDbContext
    {
        protected EFCoreRepo(IEFCoreUnitOfWork unitOfWork) : base(unitOfWork) { }

        public virtual TContext DbContext => this.DbSession.GetCtx<TContext>();
    }

    public abstract class EFCoreRepo<TContext, TEntity> : EFCoreRepo<TContext> where TContext : KnstDbContext where TEntity : class
    {
        protected EFCoreRepo(IEFCoreUnitOfWork unitOfWork) : base(unitOfWork) { }

        public DbSet<TEntity> EntitySet => this.DbContext.Set<TEntity>();
    }
}