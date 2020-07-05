using System.Collections.Generic;
using System.Threading.Tasks;
using KnstArchitecture.Repos;

namespace Toy.Repos.Example
{
    public interface IExampleRepo : IEFCoreRepo
    {
        Task<IEnumerable<Models.ExampleContextModels.Example>> GetAsync();
        Task<Models.ExampleContextModels.Example> GetAsync(int id);
        Task PostAsync(Models.ExampleContextModels.Example example);
        Task<int> PostByDapperAsync(Models.ExampleContextModels.Example example);
        Task PutAsync(Models.ExampleContextModels.Example example);
        Task DeleteAsync(Models.ExampleContextModels.Example example);
    }
}