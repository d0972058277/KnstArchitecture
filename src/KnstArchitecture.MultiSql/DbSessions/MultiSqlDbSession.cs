using System;
using System.Collections.Generic;
using System.Linq;

namespace KnstArchitecture.DbSessions
{
    public class MultiSqlDbSession : DbSession, IMultiSqlDbSession
    {
        protected Func<List<ISqlDbSession>, ISqlDbSession> _filter;
        public RichSqlDbSession SqlDbSessions { get; private set; }
        public ISqlDbSession this [int index] => SqlDbSessions.ElementAt(index);

        public MultiSqlDbSession(IDbSessionBag dbSessionBag, RichSqlDbSession sqlDbSessions) : base(dbSessionBag)
        {
            SqlDbSessions = sqlDbSessions;
        }

        /// <summary>
        /// IMultiSqlDbSession ���U�Ҧ� SqlDbSession �������� Commit
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
        /// IMultiSqlDbSession ���U�Ҧ� SqlDbSession �������� Rollback
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

        /// <summary>
        /// IMultiSqlDbSession ���U�Ҧ� SqlDbSession �������� BeginTransaction
        /// </summary>
        public new IMultiSqlDbSession BeginTransaction()
        {
            base.BeginTransaction();
            foreach (var dbSession in SqlDbSessions)
            {
                dbSession.BeginTransaction();
            }
            return this;
        }

        public ISqlDbSession First() => SqlDbSessions.First();

        public ISqlDbSession Last() => SqlDbSessions.Last();

        public void ClearDefaultFilter() => _filter = null;

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

            // ����D�U�޸귽
            foreach (var session in SqlDbSessions)
            {
                session.Dispose();
            }

            // ����U�޸귽
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