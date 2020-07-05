using System;
using Microsoft.Data.SqlClient;

namespace KnstArchitecture.DbSessions
{
    public class SqlServerDbSession : EFCoreDbSession, ISqlServerDbSession
    {
        public SqlServerDbSession(IDbSessionBag dbSessionBag, SqlConnection connection, IServiceProvider serviceProvider) : base(dbSessionBag, connection, serviceProvider) { }

        public new ISqlServerDbSession BeginTransaction()
        {
            base.BeginTransaction();
            return this;
        }
    }
}