using System.Collections.Generic;
using System.Threading.Tasks;
using KnstArchitecture.Repos;

namespace KnstApiMultiSql.Repos.Example
{
    public interface IExampleRepo : ISqlRepo
    {
        Task<IEnumerable<Models.Test.Example>> GetAsync();
    }
}