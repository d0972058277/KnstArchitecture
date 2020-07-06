using System.Data;

namespace KnstArchitecture.DbSessions
{
    public interface ISqlDbSession : IDbSession
    {
        string DataSource { get; }
        string Database { get; }
        string ConnectionString { get; }
        ConnectionState State { get; }
        new ISqlDbSession BeginTransaction();
        IDbConnection GetConnection();
        IDbTransaction GetTransaction();
        T GetConnection<T>() where T : IDbConnection;
        T GetTransaction<T>() where T : IDbTransaction;
    }
}