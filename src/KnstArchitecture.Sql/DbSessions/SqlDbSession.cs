using System.Data;
using System.Data.Common;

namespace KnstArchitecture.DbSessions
{
    public class SqlDbSession : DbSession, ISqlDbSession
    {
        protected IDbConnection _connection;
        protected IDbTransaction _transaction;

        public SqlDbSession(IDbSessionBag dbSessionBag, IDbConnection connection) : base(dbSessionBag)
        {
            _connection = connection;
        }

        public string DataSource => (_connection as DbConnection).DataSource;

        public string Database => (_connection as DbConnection).Database;

        public string ConnectionString => (_connection as DbConnection).ConnectionString;

        public ConnectionState State => (_connection as DbConnection).State;

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            // 釋放非託管資源
            _connection?.Dispose();
            _connection = null;
            _transaction?.Dispose();
            _transaction = null;

            base.Dispose(disposing);
        }

        public override void Commit()
        {
            base.Commit();
            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
            _connection.Close();
        }

        public override void Rollback()
        {
            base.Rollback();
            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
            _connection.Close();
        }

        public new ISqlDbSession BeginTransaction()
        {
            base.BeginTransaction();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            return this;
        }

        public IDbConnection GetConnection() => _connection;

        public IDbTransaction GetTransaction() => _transaction;

        public T GetConnection<T>() where T : IDbConnection => (T) GetConnection();

        public T GetTransaction<T>() where T : IDbTransaction => (T) GetTransaction();
    }
}