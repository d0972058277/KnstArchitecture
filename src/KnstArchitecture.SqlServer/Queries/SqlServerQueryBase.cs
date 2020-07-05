namespace KnstArchitecture.Queries
{
    public abstract class SqlServerQueryBase : QueryBase
    {
        protected SqlServerQueryBase(SqlServerQueryConnectionFactory queryConnectionFactory) : base(queryConnectionFactory) { }
    }
}