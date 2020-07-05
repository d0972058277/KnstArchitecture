using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using KnstArchitecture.Repos;
using KnstArchitecture.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace Toy.Repos.Example
{
    public class ExampleRepo : EFCoreRepo<Models.ExampleContextModels.ExampleContext, Models.ExampleContextModels.Example>, IExampleRepo
    {
        public ExampleRepo(IEFCoreUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task DeleteAsync(Models.ExampleContextModels.Example example)
        {
            EntitySet.Remove(example);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Models.ExampleContextModels.Example>> GetAsync()
        {
            var result = await EntitySet.ToListAsync();
            return result;
        }

        public async Task<Models.ExampleContextModels.Example> GetAsync(int id)
        {
            var result = await EntitySet.FindAsync(id);
            return result;
        }

        public async Task PostAsync(Models.ExampleContextModels.Example example)
        {
            await EntitySet.AddAsync(example);
        }

        public async Task<int> PostByDapperAsync(Models.ExampleContextModels.Example example)
        {

            string sql = "INSERT INTO Test.example (name, status) VALUES (@name, @status);" +
                "SELECT LAST_INSERT_ID();";
            var result = await Connection.QuerySingleAsync<int>(sql, example, Transaction);
            return result;
        }

        public async Task PutAsync(Models.ExampleContextModels.Example example)
        {
            EntitySet.Update(example);
            await Task.CompletedTask;
        }
    }
}