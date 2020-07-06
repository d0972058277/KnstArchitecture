using System;
using System.Data;

namespace KnstArchitecture.DbSessions
{
    public class SqliteDbSession : EFCoreDbSession, ISqliteDbSession
    {
        public SqliteDbSession(IDbSessionBag dbSessionBag, IDbConnection connection, IServiceProvider serviceProvider) : base(dbSessionBag, connection, serviceProvider) { }

        public new ISqliteDbSession BeginTransaction()
        {
            base.BeginTransaction();
            return this;
        }
    }
}