using System;
using System.Collections.Generic;
using System.Linq;

namespace KnstArchitecture.DbSessions
{
    public class MultiSqlDbSession : DbSession, IMultiSqlDbSession
    {
        protected Func<List<ISqlDbSession>, ISqlDbSession> _filter;
        public RichSqlDbSession SqlDbSessions { get; private set; }

        public Func<List<ISqlDbSession>, ISqlDbSession> DefaultFilter { get => this._filter; }

        public ISqlDbSession this [int index] => SqlDbSessions.ElementAt(index);

        public MultiSqlDbSession(IDbSessionBag dbSessionBag, RichSqlDbSession sqlDbSessions) : base(dbSessionBag)
        {
            SqlDbSessions = sqlDbSessions;
        }

        /// <summary>
        /// IMultiSqlDbSession 底下所有 SqlDbSession 全部執行 BeginTransaction
        /// </summary>
        public override IDbSession BeginTransaction()
        {
            base.BeginTransaction();
            foreach (var dbSession in SqlDbSessions)
            {
                dbSession.BeginTransaction();
            }
            return this;
        }

        /// <summary>
        /// IMultiSqlDbSession 底下所有 SqlDbSession 全部執行 Commit
        /// </summary>
        public override void Commit()
        {
            base.Commit();
            foreach (var dbSession in SqlDbSessions)
            {
                if (dbSession.IsTransaction)
                    dbSession.Commit();
            }
        }

        /// <summary>
        /// IMultiSqlDbSession 底下所有 SqlDbSession 全部執行 Rollback
        /// </summary>
        public override void Rollback()
        {
            base.Rollback();
            foreach (var dbSession in SqlDbSessions)
            {
                if (dbSession.IsTransaction)
                    dbSession.Rollback();
            }
        }

        IMultiSqlDbSession IMultiSqlDbSession.BeginTransaction() => this.BeginTransaction() as IMultiSqlDbSession;

        public ISqlDbSession First() => SqlDbSessions.First();

        public ISqlDbSession Last() => SqlDbSessions.Last();

        public void RemoveDefaultFilter() => _filter = null;

        public void SetDefaultFilter(Func<List<ISqlDbSession>, ISqlDbSession> filter) => _filter = filter;

        public ISqlDbSession Default()
        {
            if (_filter is null)
            {
                throw new InvalidOperationException("Default filter has not been set. Please use SetPickFilter(Func<List<ISqlDbSession>,ISqlDbSession> filter) to set.");
            }
            var result = _filter(SqlDbSessions);
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            // 釋放非託管資源
            foreach (var session in SqlDbSessions)
            {
                session.Dispose();
            }

            // 釋放託管資源
            if (disposing)
            {
                SqlDbSessions.Clear();
                SqlDbSessions = null;
                _filter = null;
            }

            base.Dispose(disposing);
        }
    }
}