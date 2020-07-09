using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using KnstArchitecture.Repos;
using KnstArchitecture.UnitOfWorks;

namespace KnstApiMultiSql.Repos.Example
{
    public class ExampleRepo : SqlRepo, IExampleRepo
    {
        public ExampleRepo(ISqlUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Task<IEnumerable<Models.Test.Example>> GetAsync()
        {
            var result = Connection.QueryAsync<Models.Test.Example>("your sql", null, Transaction);
            return result;
        }
    }
}