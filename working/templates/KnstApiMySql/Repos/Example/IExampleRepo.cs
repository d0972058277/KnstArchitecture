using System.Threading.Tasks;
using KnstArchitecture.Repos;

namespace KnstApiMySql.Repos.Example
{
    public interface IExampleRepo : IEFCoreRepo
    {
        Task<Models.Test.Example> GetExampleAsync(int id);
        Task InsertExampleAsync(Models.Test.Example example);
        Task UpdateExampleAsync(int id, Models.Test.Example example);
        Task DeleteExampleAsync(int id);
    }
}