using System;
using System.Data;

namespace KnstArchitecture.DbSessions
{
    public class SqlServerDbSession : EFCoreDbSession, ISqlServerDbSession
    {
        public SqlServerDbSession(IDbSessionBag dbSessionBag, IDbConnection connection, IServiceProvider serviceProvider) : base(dbSessionBag, connection, serviceProvider) { }

        ISqlServerDbSession ISqlServerDbSession.BeginTransaction() => base.BeginTransaction() as ISqlServerDbSession;
    }
}