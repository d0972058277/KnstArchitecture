namespace KnstArchitecture.Queries
{
    public abstract class MySqlQueryBase : QueryBase
    {
        protected MySqlQueryBase(MySqlQueryConnectionFactory queryConnectionBuilder) : base(queryConnectionBuilder) { }
    }
}