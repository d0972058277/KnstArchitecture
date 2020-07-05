using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using KnstArchitecture.Queries;

namespace Toy.Queries.Example
{
    public class ExampleQuery : MySqlQueryBase, IExampleQuery
    {
        public ExampleQuery(MySqlQueryConnectionFactory queryConnectionBuilder) : base(queryConnectionBuilder) { }

        public async Task<IEnumerable<Models.ExampleContextModels.Example>> GetAsync()
        {
            string sql = "select * from Test.example";
            var result = await Connection.QueryAsync<Models.ExampleContextModels.Example>(sql, null);
            return result;
        }

        public async Task<Models.ExampleContextModels.Example> GetAsync(int id)
        {
            string sql = "select * from Test.example where id = @id";
            var result = await Connection.QuerySingleOrDefaultAsync<Models.ExampleContextModels.Example>(sql, new { id });
            return result;
        }
    }
}