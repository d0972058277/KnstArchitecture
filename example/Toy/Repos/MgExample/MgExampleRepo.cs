using System.Collections.Generic;
using System.Threading.Tasks;
using KnstArchitecture;
using KnstArchitecture.Repos;
using KnstArchitecture.UnitOfWorks;
using MongoDB.Driver;

namespace Toy.Repos.MgExample
{
    public class MgExampleRepo : MongoRepo, IMgExampleRepo
    {
        public MgExampleRepo(IMongoUnitOfWork unitOfWork) : base(unitOfWork) { }

        IMongoCollection<Models.MongoTest.MgExample> MongoCollection => MongoClient.GetDatabase("Test").GetCollection<Models.MongoTest.MgExample>("Example");
        public async Task<DeleteResult> DeleteAsync(string id)
        {
            var filter = Builders<Models.MongoTest.MgExample>.Filter.Eq(x => x.Id, id);

            var result = await MongoCollection.DeleteOneAsync(filter, ClientSessionHandle);
            return result;
        }

        public async Task<IEnumerable<Models.MongoTest.MgExample>> GetAsync()
        {
            var filter = Builders<Models.MongoTest.MgExample>.Filter.Empty;

            var find = await MongoCollection.FindAsync(filter, ClientSessionHandle);

            var result = await find.ToListAsync();
            return result;
        }

        public async Task<Models.MongoTest.MgExample> GetAsync(string id)
        {
            var filter = Builders<Models.MongoTest.MgExample>.Filter.Eq(x => x.Id, id);

            var find = await MongoCollection.FindAsync(filter, ClientSessionHandle);

            var result = await find.SingleOrDefaultAsync();
            return result;
        }

        public async Task PostAsync(Models.MongoTest.MgExample example)
        {
            await MongoCollection.InsertOneAsync(example, ClientSessionHandle);
        }

        public async Task<UpdateResult> PutAsync(Models.MongoTest.MgExample example)
        {
            var filter = Builders<Models.MongoTest.MgExample>.Filter.Eq(x => x.Id, example.Id);
            var update = Builders<Models.MongoTest.MgExample>.Update.Set(x => x.Name, example.Name)
                .Set(x => x.RowDatetime, example.RowDatetime)
                .Set(x => x.IsDelete, example.IsDelete);

            var result = await MongoCollection.UpdateOneAsync(filter, update, ClientSessionHandle);
            return result;
        }
    }
}