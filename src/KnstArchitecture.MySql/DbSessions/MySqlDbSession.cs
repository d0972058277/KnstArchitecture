using System.Data;
using System;

namespace KnstArchitecture.DbSessions
{
    public class MySqlDbSession : EFCoreDbSession, IMySqlDbSession
    {
        public MySqlDbSession(IDbSessionBag dbSessionBag, IDbConnection connection, IServiceProvider serviceProvider) : base(dbSessionBag, connection, serviceProvider) { }

        public new IMySqlDbSession BeginTransaction()
        {
            base.BeginTransaction();
            return this;
        }
    }
}