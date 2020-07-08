using System;
using KnstArchitecture.DbSessions;

namespace KnstArchitecture.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IDbSession CreateDbSession();
        IDbSession GetDefaultDbSession();
        void BeginTransaction();
        void Commit();
        void Rollback();
        void BeginTransaction(IDbSession dbSession);
        void Commit(IDbSession dbSession);
        void Rollback(IDbSession dbSession);
    }
}