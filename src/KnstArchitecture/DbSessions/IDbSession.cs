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

    public interface IDbSession<TConnection, TTransaction> : IDbSession
    {
        TConnection GetConnection();
        TTransaction GetTransaction();
        T GetConnection<T>() where T : TConnection;
        T GetTransaction<T>() where T : TTransaction;
    }
}