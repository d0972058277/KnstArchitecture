using System;
using Microsoft.Data.SqlClient;

namespace KnstArchitecture.Queries
{
    public class SqlServerQueryConnectionFactory : SqlQueryConnectionFactory
    {
        public SqlServerQueryConnectionFactory(Func<SqlConnection> func) : base(func) { }
    }
}