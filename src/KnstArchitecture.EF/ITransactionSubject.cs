using KnstArchitecture.EF.DbContexts;

namespace KnstArchitecture.EF
{
    public interface ITransactionSubject
    {
        void Attach(KnstDbContext observer);
        void Detach(KnstDbContext observer);
        void NotifyObserverUseTransaction();
    }
}