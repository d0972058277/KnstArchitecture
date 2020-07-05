using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using KnstArchitecture.Queries;

namespace KnstApi.Queries.Example
{
    public class ExampleQuery : QueryBase, IExampleQuery
    {
        public ExampleQuery(IQueryConnectionBuilder queryConnectionBuilder) : base(queryConnectionBuilder) { }

        public async Task<IEnumerable<Models.Test.Example>> GetExamplesAsync()
        {
            string sql = "select * from Test.example";
            var result = await Connection.QueryAsync<Models.Test.Example>(sql, null);
            return result;
        }
    }
}