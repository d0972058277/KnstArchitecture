using System.Collections.Generic;
using System.Threading.Tasks;
using KnstArchitecture.Queries;

namespace Toy.Queries.Example
{
    public interface IExampleQuery : IQuery
    {
        Task<IEnumerable<Models.ExampleContextModels.Example>> GetAsync();
        Task<Models.ExampleContextModels.Example> GetAsync(int id);
    }
}