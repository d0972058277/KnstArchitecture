using System;
using System.Data;

namespace KnstArchitecture.Queries
{
    public abstract class QueryBase : IQuery
    {
        protected bool _disposed = false;

        protected QueryBase(IQueryConnectionFactory queryConnectionFactory)
        {
            Connection = queryConnectionFactory.Get();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            Connection?.Dispose();
            Connection = null;

            _disposed = true;
        }

        public IDbConnection Connection { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}