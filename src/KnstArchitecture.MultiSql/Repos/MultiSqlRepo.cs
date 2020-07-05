using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KnstArchitecture.DbSessions;
using KnstArchitecture.UnitOfWorks;

namespace KnstArchitecture.Repos
{
    public class MultiSqlRepo : Repo, IMultiSqlRepo
    {
        public MultiSqlRepo(IMultiSqlUnitOfWork unitOfWork) : base(unitOfWork) { }

        public ISqlDbSession this [int index] => this.DbSession[index];

        public IDbConnection DefaultConnection => this.Default().GetConnection();

        public IDbTransaction DefaultTransaction => this.Default().GetTransaction();

        public IMultiSqlDbSession DbSession => (this as IRepo).DbSession as IMultiSqlDbSession;

        public List<IDbConnection> Connections => this.DbSession.SqlDbSessions.Select(s => s.GetConnection()).ToList();

        public List<IDbTransaction> Transactions => this.DbSession.SqlDbSessions.Select(s => s.GetTransaction()).ToList();

        public void ClearDefaultFilter() => this.DbSession.ClearDefaultFilter();

        public ISqlDbSession Default() => this.DbSession.Default();

        public ISqlDbSession First() => this.DbSession.First();

        public ISqlDbSession Last() => this.DbSession.Last();

        public void SetDefaultFilter(Func<List<ISqlDbSession>, ISqlDbSession> filter) => this.DbSession.SetDefaultFilter(filter);
    }
}