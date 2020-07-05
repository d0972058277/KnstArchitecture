using System;
using MySql.Data.MySqlClient;

namespace KnstArchitecture.DbSessions
{
    public class MySqlDbSession : EFCoreDbSession, IMySqlDbSession
    {
        public MySqlDbSession(IDbSessionBag dbSessionBag, MySqlConnection connection, IServiceProvider serviceProvider) : base(dbSessionBag, connection, serviceProvider) { }

        public new IMySqlDbSession BeginTransaction()
        {
            base.BeginTransaction();
            return this;
        }
    }
}