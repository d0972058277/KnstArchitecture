using System.Data;
using KnstArchitecture.Queries;
using MySql.Data.MySqlClient;

namespace KnstApi.Queries
{
    public class QueryConnectionBuilder : QueryConnectionBuilderBase
    {
        public QueryConnectionBuilder() { }

        public QueryConnectionBuilder(string connectionString) : base(connectionString) { }

        public override IDbConnection Build() => new MySqlConnection(_connectionString);
    }
}