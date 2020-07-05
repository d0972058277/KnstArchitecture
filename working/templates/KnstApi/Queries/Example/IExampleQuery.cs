using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnstApi.Queries.Example
{
    public interface IExampleQuery
    {
        Task<IEnumerable<Models.Test.Example>> GetExamplesAsync();
    }
}