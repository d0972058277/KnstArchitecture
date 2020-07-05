using System.Collections.Generic;
using System.Data;
using KnstArchitecture.DbSessions;
using KnstArchitecture.MultiSql;

namespace KnstArchitecture.Repos
{
    public interface IMultiSqlRepo : IRepo, IMultiSqlDefault, IMultiSqlPick
    {
        new IMultiSqlDbSession DbSession { get; }
        List<IDbConnection> Connections { get; }
        List<IDbTransaction> Transactions { get; }
        IDbConnection DefaultConnection { get; }
        IDbTransaction DefaultTransaction { get; }
    }
}