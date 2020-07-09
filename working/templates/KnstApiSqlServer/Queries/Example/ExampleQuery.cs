using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using KnstArchitecture.Queries;

namespace KnstApiSqlServer.Queries.Example
{
    public class ExampleQuery : SqlServerQueryBase, IExampleQuery
    {
        public ExampleQuery(SqlServerQueryConnectionFactory queryConnectionFactory) : base(queryConnectionFactory) { }

        public async Task<IEnumerable<Models.Test.Example>> GetExamplesAsync()
        {
            string sql = "select * from Test.example";
            var result = await Connection.QueryAsync<Models.Test.Example>(sql, null);
            return result;
        }
    }
}