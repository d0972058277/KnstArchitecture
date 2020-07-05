using System;
using Microsoft.Data.Sqlite;

namespace KnstArchitecture.Queries
{
    public class SqliteQueryConnectionFactory : SqlQueryConnectionFactory
    {
        public SqliteQueryConnectionFactory(Func<SqliteConnection> func) : base(func) { }
    }
}