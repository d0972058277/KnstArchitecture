using System.Data;

namespace KnstArchitecture.Queries
{
    public interface IQueryConnectionFactory
    {
        IDbConnection Get();
    }
}