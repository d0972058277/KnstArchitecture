using System;
using System.Collections.Generic;
using KnstArchitecture.DbSessions;

namespace KnstArchitecture.MultiSql
{
    public interface IMultiSqlDefault
    {
        Func<List<ISqlDbSession>, ISqlDbSession> DefaultFilter { get; }
        void RemoveDefaultFilter();
        void SetDefaultFilter(Func<List<ISqlDbSession>, ISqlDbSession> filter);
        ISqlDbSession Default();
    }
}