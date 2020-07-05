namespace KnstArchitecture.Queries
{
    public abstract class SqliteQueryBase : QueryBase
    {
        protected SqliteQueryBase(SqliteQueryConnectionFactory queryConnectionFactory) : base(queryConnectionFactory) { }
    }
}