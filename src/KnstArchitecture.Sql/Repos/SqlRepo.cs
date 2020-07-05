using System.Data;
using KnstArchitecture.DbSessions;
using KnstArchitecture.UnitOfWorks;

namespace KnstArchitecture.Repos
{
    public class SqlRepo : Repo, ISqlRepo
    {
        public SqlRepo(ISqlUnitOfWork unitOfWork) : base(unitOfWork) { }

        public IDbConnection Connection => this.DbSession.GetConnection();

        public IDbTransaction Transaction => this.DbSession.GetTransaction();

        public ISqlDbSession DbSession => (this as IRepo).DbSession as ISqlDbSession;
    }
}