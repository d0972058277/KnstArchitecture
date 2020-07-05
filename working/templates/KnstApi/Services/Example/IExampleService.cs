using System.Threading.Tasks;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Services;

namespace KnstApi.Services.Example
{
    public interface IExampleService : IService
    {
        Task<Models.Test.Example> InsertThenGetAsync(Models.Test.Example example);
        Task<Models.Test.Example> InsertThenGetAsync(ISqlDbSession dbSession, Models.Test.Example example);
    }
}