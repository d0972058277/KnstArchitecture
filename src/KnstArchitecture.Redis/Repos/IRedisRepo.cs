using KnstArchitecture.DbSessions;
using StackExchange.Redis;

namespace KnstArchitecture.Repos
{
    public interface IRedisRepo : IRepo
    {
        new IRedisDbSession DbSession { get; }
        ITransaction Transaction { get; }
        IDatabase Database(int db = -1);
    }
}
