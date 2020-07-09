using System.Threading.Tasks;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Services;

namespace KnstApiSqlServer.Services.Example
{
    public interface IExampleService : IService
    {
        Task<Models.Test.Example> InsertThenGetAsync(Models.Test.Example example);
        Task<Models.Test.Example> InsertThenGetAsync(ISqlServerDbSession dbSession, Models.Test.Example example);
    }
}