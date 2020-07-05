using KnstArchitecture.DbSessions;
using KnstArchitecture.UnitOfWorks;
using MongoDB.Driver;

namespace KnstArchitecture.Repos
{
    public class MongoRepo : Repo, IMongoRepo
    {
        public MongoRepo(IMongoUnitOfWork unitOfWork) : base(unitOfWork) { }

        IMongoDbSession IMongoRepo.DbSession => (this as IRepo).DbSession as IMongoDbSession;

        public IMongoClient MongoClient => (this as IMongoRepo).DbSession.GetMongoClient();

        public IClientSessionHandle ClientSessionHandle => (this as IMongoRepo).DbSession.GetClientSessionHandle();
    }
}