using System;
using System.Data;

namespace KnstArchitecture.Queries
{
    public class SqlQueryConnectionFactory : IQueryConnectionFactory
    {
        private Func<IDbConnection> _func;

        public SqlQueryConnectionFactory(Func<IDbConnection> func)
        {
            _func = func;
        }

        public virtual IDbConnection Get() => _func();
    }
}