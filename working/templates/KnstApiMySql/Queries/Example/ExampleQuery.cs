using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using KnstArchitecture.Queries;

namespace KnstApiMySql.Queries.Example
{
    public class ExampleQuery : MySqlQueryBase, IExampleQuery
    {
        public ExampleQuery(MySqlQueryConnectionBuilder queryConnectionBuilder) : base(queryConnectionBuilder) { }

        public async Task<IEnumerable<Models.Test.Example>> GetExamplesAsync()
        {
            string sql = "select * from Test.example";
            var result = await Connection.QueryAsync<Models.Test.Example>(sql, null);
            return result;
        }
    }
}