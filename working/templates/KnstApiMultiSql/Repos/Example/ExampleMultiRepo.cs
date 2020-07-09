using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using KnstArchitecture.Repos;
using KnstArchitecture.UnitOfWorks;

namespace KnstApiMultiSql.Repos.Example
{
    public class ExampleMultiRepo : MultiSqlRepo, IExampleMultiRepo
    {
        public ExampleMultiRepo(IMultiSqlUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Task<IEnumerable<Models.Test.Example>> DefaultGetAsync()
        {
            IDbConnection connection = DbSession.Default().GetConnection();
            IDbTransaction transaction = DbSession.Default().GetTransaction();

            // connection 等於 DefaultConnection
            // DefaultConnection 的內部程式碼就是 DbSession.Default().GetConnection();

            // transaction 等於 DefaultTransaction
            // DefaultTransaction 的內部程式碼就是 DbSession.Default().GetTransaction();

            // 這兩行是相同的
            // var result = connection.QueryAsync<Models.Test.Example>("your sql", null, transaction);
            var result = DefaultConnection.QueryAsync<Models.Test.Example>("your sql", null, DefaultTransaction);
            return result;
        }

        public Task<IEnumerable<Models.Test.Example>> FirstGetAsync()
        {
            IDbConnection connection = DbSession.First().GetConnection();
            IDbTransaction transaction = DbSession.First().GetTransaction();
            var result = connection.QueryAsync<Models.Test.Example>("your sql", null, transaction);
            return result;
        }

        public Task<IEnumerable<Models.Test.Example>> SecondGetAsync()
        {
            IDbConnection connection = DbSession[1].GetConnection();
            IDbTransaction transaction = DbSession[1].GetTransaction();
            var result = connection.QueryAsync<Models.Test.Example>("your sql", null, transaction);
            return result;
        }

        public Task<IEnumerable<Models.Test.Example>> ThirdGetAsync()
        {
            IDbConnection connection = DbSession.Last().GetConnection();
            IDbTransaction transaction = DbSession.Last().GetTransaction();
            var result = connection.QueryAsync<Models.Test.Example>("your sql", null, transaction);
            return result;
        }
    }
}