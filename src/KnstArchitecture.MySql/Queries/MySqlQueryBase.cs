namespace KnstArchitecture.Queries
{
    public abstract class MySqlQueryBase : QueryBase
    {
        protected MySqlQueryBase(MySqlQueryConnectionFactory queryConnectionFactory) : base(queryConnectionFactory) { }
    }
}