using KnstArchitecture.DbSessions;
using KnstArchitecture.UnitOfWorks;
using StackExchange.Redis;

namespace KnstArchitecture.Repos
{
    public class RedisRepo : Repo, IRedisRepo
    {
        public RedisRepo(IRedisUnitOfWork unitOfWork) : base(unitOfWork) { }

        public IRedisDbSession DbSession => (this as IRepo).DbSession as IRedisDbSession;

        public ITransaction Transaction => DbSession.GetTransaction();

        public IDatabase Database(int db = -1) => DbSession.GetConnectionMultiplexer().GetDatabase(db);
    }
}