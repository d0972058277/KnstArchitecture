using System.Threading.Tasks;
using KnstApiSqlServer.Repos.Example;
using KnstArchitecture.DbSessions;
using KnstArchitecture.UnitOfWorks;

namespace KnstApiSqlServer.Services.Example
{
    public class ExampleService : IExampleService
    {
        ISqlServerUnitOfWork UnitOfWork;

        public ExampleService(ISqlServerUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<Models.Test.Example> InsertThenGetAsync(Models.Test.Example example)
        {
            await UnitOfWork.Use<IExampleRepo>().InsertExampleAsync(example);
            UnitOfWork.SaveChanges();
            var result = await UnitOfWork.Use<IExampleRepo>().GetExampleAsync(example.Id);
            return result;
        }

        public async Task<Models.Test.Example> InsertThenGetAsync(ISqlServerDbSession dbSession, Models.Test.Example example)
        {
            await UnitOfWork.Use<IExampleRepo>(dbSession).InsertExampleAsync(example);

            // Equal to: session.SaveChanges();
            UnitOfWork.SaveChanges(dbSession);

            var result = await UnitOfWork.Use<IExampleRepo>(dbSession).GetExampleAsync(example.Id);
            return result;
        }
    }
}