using System.Data;
using KnstArchitecture.DbSessions;

namespace KnstArchitecture.Repos
{
    public interface ISqlRepo : IRepo
    {
        new ISqlDbSession DbSession { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
    }
}