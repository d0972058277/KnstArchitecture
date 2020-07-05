using MongoDB.Driver;

namespace KnstArchitecture.DbSessions
{
    public interface IMongoDbSession : IDbSession
    {
        new IMongoDbSession BeginTransaction();
        IMongoClient GetMongoClient();
        IClientSessionHandle GetClientSessionHandle();
    }
}
