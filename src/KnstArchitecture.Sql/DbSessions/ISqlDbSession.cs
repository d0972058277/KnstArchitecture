using System.Data;

namespace KnstArchitecture.DbSessions
{
    public interface ISqlDbSession : IDbSession<IDbConnection, IDbTransaction>
    {
        string DataSource { get; }
        string Database { get; }
        string ConnectionString { get; }
        ConnectionState State { get; }
        new ISqlDbSession BeginTransaction();
    }
}