using System.Threading.Tasks;
using KnstApiMySql.Repos.Example;
using KnstArchitecture.DbSessions;
using KnstArchitecture.UnitOfWorks;

namespace KnstApiMySql.Services.Example
{
    public class ExampleService : IExampleService
    {
        IMySqlUnitOfWork UnitOfWork;

        public ExampleService(IMySqlUnitOfWork unitOfWork)
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

        public async Task<Models.Test.Example> InsertThenGetAsync(IMySqlDbSession dbSession, Models.Test.Example example)
        {
            await UnitOfWork.Use<IExampleRepo>(dbSession).InsertExampleAsync(example);

            // Equal to: session.SaveChanges();
            UnitOfWork.SaveChanges(dbSession);

            var result = await UnitOfWork.Use<IExampleRepo>(dbSession).GetExampleAsync(example.Id);
            return result;
        }
    }
}