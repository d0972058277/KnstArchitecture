using KnstArchitecture.DbSessions;

namespace KnstArchitecture.MultiSql
{
    public interface IMultiSqlPick
    {
        ISqlDbSession First();
        ISqlDbSession Last();
        ISqlDbSession this [int index] { get; }
    }
}