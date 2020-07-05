using System;
using MySql.Data.MySqlClient;

namespace KnstArchitecture.Queries
{
    public class MySqlQueryConnectionFactory : SqlQueryConnectionFactory
    {
        public MySqlQueryConnectionFactory(Func<MySqlConnection> func) : base(func) { }
    }
}