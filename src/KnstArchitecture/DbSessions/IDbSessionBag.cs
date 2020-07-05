using System;

namespace KnstArchitecture.DbSessions
{
    public interface IDbSessionBag : IDisposable
    {
        void Add(IDbSession dbSession);
        void Remove(IDbSession dbSession);
        void Commit();
        void Rollback();
        bool Empty { get; }
        bool Any { get; }
        int Count { get; }
    }
}