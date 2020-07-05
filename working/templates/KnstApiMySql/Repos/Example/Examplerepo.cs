using System.Threading.Tasks;
using KnstArchitecture.Repos;

namespace KnstApiMySql.Repos.Example
{
    public class Examplerepo : EFCoreRepo<Models.Test.TestContext, Models.Test.Example>, IExampleRepo
    {
        public async Task DeleteExampleAsync(int id)
        {
            var example = await EntitySet.FindAsync(id);
            if (example is null) return;
            EntitySet.Remove(example);
        }

        public async Task<Models.Test.Example> GetExampleAsync(int id)
        {
            var result = await EntitySet.FindAsync(id);
            return result;
        }

        public async Task InsertExampleAsync(Models.Test.Example example)
        {
            await EntitySet.AddAsync(example);
        }

        public async Task UpdateExampleAsync(int id, Models.Test.Example example)
        {
            example.Id = id;
            EntitySet.Update(example);
            await Task.CompletedTask;
        }
    }
}