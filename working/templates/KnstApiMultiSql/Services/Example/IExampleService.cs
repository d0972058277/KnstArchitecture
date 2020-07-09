using System.Collections.Generic;
using System.Threading.Tasks;
using KnstArchitecture.Services;

namespace KnstApiMultiSql.Services.Example
{
    public interface IExampleService
    {
        #region IMultiSqlRepo
        Task<IEnumerable<Models.Test.Example>> FirstGetAsync();
        Task<IEnumerable<Models.Test.Example>> SecondGetAsync();
        Task<IEnumerable<Models.Test.Example>> ThirdGetAsync();
        Task<IEnumerable<Models.Test.Example>> MultiRepoDefaultGetAsync();
        #endregion
        #region ISqlRepo
        Task<IEnumerable<Models.Test.Example>> RepoDefaultGetAsync();
        #endregion
    }
}