using KnstArchitecture.DbSessions;
using MongoDB.Driver;

namespace KnstArchitecture.Repos
{
    public interface IMongoRepo : IRepo
    {
        new IMongoDbSession DbSession { get; }
        IMongoClient MongoClient { get; }
        IClientSessionHandle ClientSessionHandle { get; }
    }
}
