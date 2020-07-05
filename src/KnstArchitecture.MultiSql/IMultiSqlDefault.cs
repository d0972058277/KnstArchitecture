using System;
using System.Collections.Generic;
using KnstArchitecture.DbSessions;

namespace KnstArchitecture.MultiSql
{
    public interface IMultiSqlDefault
    {
        void ClearDefaultFilter();
        void SetDefaultFilter(Func<List<ISqlDbSession>, ISqlDbSession> filter);
        ISqlDbSession Default();
    }
}