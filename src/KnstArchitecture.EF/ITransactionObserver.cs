using System.Data;

namespace KnstArchitecture.EF
{
    public interface ITransactionObserver
    {
        void UseTransaction(IDbTransaction dbTransaction);
    }
}