using KnstArchitecture.DbSessions;
using Microsoft.EntityFrameworkCore;

namespace KnstArchitecture.EF.DbContexts
{
    public interface IKnstDbContext : ITransactionObserver
    {
        IEFCoreDbSession DbSession { get; set; }
        void InnerOnConfiguring(DbContextOptionsBuilder optionsBuilder);
    }
}