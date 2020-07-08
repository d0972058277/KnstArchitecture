using MongoDB.Driver;

namespace KnstArchitecture.DbSessions
{
    public class MongoDbSession : DbSession, IMongoDbSession
    {
        protected IMongoClient _mongoClient;
        protected IClientSessionHandle _clientSessionHandle;
        public MongoDbSession(IDbSessionBag dbSessionBag, IMongoClient mongoClient) : base(dbSessionBag)
        {
            _mongoClient = mongoClient;
        }

        public override IDbSession BeginTransaction()
        {
            base.BeginTransaction();
            if (_clientSessionHandle != null)
            {
                throw new MongoException("Transaction has started.");
            }
            StartSession();
            _clientSessionHandle.StartTransaction();
            return this;
        }

        public override void Commit()
        {
            base.Commit();
            _clientSessionHandle.CommitTransaction();
            _clientSessionHandle.Dispose();
            _clientSessionHandle = null;
        }

        public IClientSessionHandle GetClientSessionHandle() => _clientSessionHandle;

        public IMongoClient GetMongoClient()
        {
            if (_clientSessionHandle is null) return _mongoClient;
            return _clientSessionHandle.Client;
        }

        public override void Rollback()
        {
            base.Rollback();
            _clientSessionHandle.AbortTransaction();
            _clientSessionHandle.Dispose();
            _clientSessionHandle = null;
        }

        protected IClientSessionHandle StartSession()
        {
            _clientSessionHandle = _mongoClient.StartSession();
            return _clientSessionHandle;
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            _mongoClient = null;
            _clientSessionHandle?.Dispose();
            _clientSessionHandle = null;

            base.Dispose(disposing);
        }

        IMongoDbSession IMongoDbSession.BeginTransaction() => this.BeginTransaction() as IMongoDbSession;
    }
}