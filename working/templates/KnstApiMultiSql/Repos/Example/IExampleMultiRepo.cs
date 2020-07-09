using System.Collections.Generic;
using System.Threading.Tasks;
using KnstArchitecture.Repos;

namespace KnstApiMultiSql.Repos.Example
{
    public interface IExampleMultiRepo : IMultiSqlRepo
    {
        Task<IEnumerable<Models.Test.Example>> FirstGetAsync();
        Task<IEnumerable<Models.Test.Example>> SecondGetAsync();
        Task<IEnumerable<Models.Test.Example>> ThirdGetAsync();
        Task<IEnumerable<Models.Test.Example>> DefaultGetAsync();
    }
}