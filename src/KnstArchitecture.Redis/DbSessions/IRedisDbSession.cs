using StackExchange.Redis;

namespace KnstArchitecture.DbSessions
{
    public interface IRedisDbSession : IDbSession
    {
        IRedisDbSession BeginTransaction(int db = -1);
        ConnectionMultiplexer GetConnectionMultiplexer();
        IDatabase GetDatabase(int db = -1);
        ITransaction GetTransaction();
    }
}