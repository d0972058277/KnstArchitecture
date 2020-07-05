using System.Transactions;
using StackExchange.Redis;

namespace KnstArchitecture.DbSessions
{
    public class RedisDbSession : DbSession, IRedisDbSession
    {
        protected ConnectionMultiplexer _connectionMultiplexer;
        protected int? _db;
        protected ITransaction _transaction;

        public RedisDbSession(IDbSessionBag dbSessionBag, ConnectionMultiplexer connectionMultiplexer) : base(dbSessionBag)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public IRedisDbSession BeginTransaction(int db = -1)
        {
            base.BeginTransaction();
            if (_transaction != null)
                throw new RedisException($"Index {_db} database transaction has started.");
            _db = db;
            _transaction = _connectionMultiplexer.GetDatabase(db).CreateTransaction();
            return this;
        }

        public override IDbSession BeginTransaction() => this.BeginTransaction();

        public override void Commit()
        {
            try
            {
                if (!_transaction.Execute())
                    throw new RedisException($"Index {_db} database transaction commitment error.");
            }
            catch
            {
                throw;
            }
            finally
            {
                base.Commit();
                _db = null;
                _transaction = null;
            }
        }

        public ConnectionMultiplexer GetConnectionMultiplexer() => _connectionMultiplexer;

        public IDatabase GetDatabase(int db = -1) => _connectionMultiplexer.GetDatabase(db);

        public ITransaction GetTransaction() => _transaction;

        public override void Rollback()
        {
            base.Rollback();
            _db = null;
            _transaction = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            _connectionMultiplexer = null;
            _transaction = null;
            _db = null;

            base.Dispose(disposing);
        }
    }
}