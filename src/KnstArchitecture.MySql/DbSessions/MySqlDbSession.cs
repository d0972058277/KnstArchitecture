using System;
using System.Data;

namespace KnstArchitecture.DbSessions
{
    public class MySqlDbSession : EFCoreDbSession, IMySqlDbSession
    {
        public MySqlDbSession(IDbSessionBag dbSessionBag, IDbConnection connection, IServiceProvider serviceProvider) : base(dbSessionBag, connection, serviceProvider) { }

        IMySqlDbSession IMySqlDbSession.BeginTransaction() => base.BeginTransaction() as IMySqlDbSession;
    }
}