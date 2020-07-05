using System.Threading.Tasks;
using KnstApi.Repos.Example;
using KnstArchitecture.DbSessions;
using KnstArchitecture.UnitOfWorks;

namespace KnstApi.Services.Example
{
    public class ExampleService : IExampleService
    {
        ISqlUnitOfWork UnitOfWork;

        public ExampleService(ISqlUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<Models.Test.Example> InsertThenGetAsync(Models.Test.Example example)
        {
            var id = await UnitOfWork.Use<IExampleRepo>().InsertExampleAsync(example);
            var result = await UnitOfWork.Use<IExampleRepo>().GetExampleAsync(id);
            return result;
        }

        public async Task<Models.Test.Example> InsertThenGetAsync(ISqlDbSession dbSession, Models.Test.Example example)
        {
            var id = await UnitOfWork.Use<IExampleRepo>(dbSession).InsertExampleAsync(example);
            var result = await UnitOfWork.Use<IExampleRepo>(dbSession).GetExampleAsync(id);
            return result;
        }
    }
}