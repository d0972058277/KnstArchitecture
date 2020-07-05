using System;
using System.Collections.Generic;
using System.Linq;

namespace KnstArchitecture.DbSessions
{
    public class DbSessionBag : IDbSessionBag
    {
        private readonly static object @lock = new object();
        private bool _disposed = false;
        private HashSet<IDbSession> _dbSessions = new HashSet<IDbSession>();

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _dbSessions.Clear();
                _dbSessions = null;
            }

            _disposed = true;
        }

        public int Count => _dbSessions.Count;

        public bool Any => _dbSessions.Any();

        public bool Empty => !Any;

        public void Add(IDbSession dbSession)
        {
            lock (@lock)
            {
                _dbSessions.Add(dbSession);
            }
        }

        public void Remove(IDbSession dbSession)
        {
            lock (@lock)
            {
                _dbSessions.Remove(dbSession);
            }
        }

        public void Commit()
        {

            foreach (var session in _dbSessions)
            {
                // 不自動執行SaveChanges
                //(session as IMySqlContextDbSession)?.SaveChanges();

                if (session.IsTransaction)
                {
                    session.Commit();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            foreach (var session in _dbSessions)
            {
                if (session.IsTransaction)
                {
                    session.Rollback();
                }
            }
        }
    }
}