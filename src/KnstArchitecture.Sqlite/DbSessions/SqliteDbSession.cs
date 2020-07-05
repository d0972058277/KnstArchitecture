using System;
using Microsoft.Data.Sqlite;

namespace KnstArchitecture.DbSessions
{
    public class SqliteDbSession : EFCoreDbSession, ISqliteDbSession
    {
        public SqliteDbSession(IDbSessionBag dbSessionBag, SqliteConnection connection, IServiceProvider serviceProvider) : base(dbSessionBag, connection, serviceProvider) { }

        public new ISqliteDbSession BeginTransaction()
        {
            base.BeginTransaction();
            return this;
        }
    }
}