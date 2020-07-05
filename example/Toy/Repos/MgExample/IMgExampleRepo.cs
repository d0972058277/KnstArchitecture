using System.Collections.Generic;
using System.Threading.Tasks;
using KnstArchitecture.Repos;
using MongoDB.Driver;

namespace Toy.Repos.MgExample
{
    public interface IMgExampleRepo : IMongoRepo
    {
        Task<IEnumerable<Models.MongoTest.MgExample>> GetAsync();
        Task<Models.MongoTest.MgExample> GetAsync(string id);
        Task PostAsync(Models.MongoTest.MgExample example);
        Task<UpdateResult> PutAsync(Models.MongoTest.MgExample example);
        Task<DeleteResult> DeleteAsync(string id);
    }
}