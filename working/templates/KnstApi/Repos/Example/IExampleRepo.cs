using System.Threading.Tasks;
using KnstArchitecture.Repos;

namespace KnstApi.Repos.Example
{
    public interface IExampleRepo : ISqlRepo
    {
        Task<Models.Test.Example> GetExampleAsync(int id);
        Task<int> InsertExampleAsync(Models.Test.Example example);
        Task UpdateExampleAsync(int id, Models.Test.Example example);
        Task DeleteExampleAsync(int id);
    }
}