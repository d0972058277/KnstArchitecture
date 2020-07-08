using System.Collections.Generic;
using System.Collections.ObjectModel;
using KnstArchitecture.EF.DbContexts;

namespace KnstArchitecture.EF
{
    public interface ITransactionSubject
    {
        IEnumerable<KnstDbContext> Observers { get; }
        void Attach(KnstDbContext observer);
        void Detach(KnstDbContext observer);
        void NotifyObserversUseTransaction();
    }
}