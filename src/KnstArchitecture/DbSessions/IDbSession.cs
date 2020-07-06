using System;

namespace KnstArchitecture.DbSessions
{
    public interface IDbSession : IDisposable
    {
        IDbSession BeginTransaction();
        void Commit();
        void Rollback();
        bool IsTransaction { get; }
    }
}