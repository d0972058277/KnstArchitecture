using System.Collections.Generic;
using KnstArchitecture.MultiSql;

namespace KnstArchitecture.DbSessions
{
    public interface IMultiSqlDbSession : IDbSession, IMultiSqlDefault, IMultiSqlPick
    {
        RichSqlDbSession SqlDbSessions { get; }
        new IMultiSqlDbSession BeginTransaction();
    }
}