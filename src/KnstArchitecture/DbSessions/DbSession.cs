using System;
using System.Transactions;

namespace KnstArchitecture.DbSessions
{
    public abstract class DbSession : IDbSession
    {
        protected bool _disposed = false;
        protected IDbSessionBag _dbSessionBag;

        public DbSession(IDbSessionBag dbSessionBag)
        {
            _dbSessionBag = dbSessionBag;
            _dbSessionBag.Add(this);
        }

        ~DbSession() => Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            // 釋放非託管資源
            _dbSessionBag.Dispose();
            _dbSessionBag = null;

            _disposed = true;
        }

        protected virtual void ConfirmTransactionCanBeClosed()
        {
            if (IsTransaction)
                return;
            else
            {
                throw new TransactionException($"Transaction has been disposed or Transaction has not started.");
            }
        }

        protected virtual void ConfirmTransactionCanBeOpend()
        {
            if (!IsTransaction)
                return;
            else
            {
                throw new TransactionException($"Transaction has started.");
            }
        }

        public bool IsTransaction { get; protected set; }

        public virtual IDbSession BeginTransaction()
        {
            this.ConfirmTransactionCanBeOpend();
            IsTransaction = true;
            return this;
        }

        public virtual void Commit()
        {
            this.ConfirmTransactionCanBeClosed();
            IsTransaction = false;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Rollback()
        {
            this.ConfirmTransactionCanBeClosed();
            IsTransaction = false;
        }
    }
}