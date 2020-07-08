using System;
using System.Data;
using System.Data.Common;
using KnstArchitecture.DbSessions;
using KnstArchitecture.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace KnstArchitecture.EF.DbContexts
{
    public abstract class KnstDbContext : DbContext, IKnstDbContext
    {
        private bool _defaultDbSessionIsUsed;
        private IEFCoreDbSession _dbSession;
        protected IEFCoreUnitOfWork _efUnitOfWork;

        protected KnstDbContext(IEFCoreUnitOfWork efUnitOfWork)
        {
            _efUnitOfWork = efUnitOfWork;
            _defaultDbSessionIsUsed = true;
            _dbSession = _efUnitOfWork.GetDefaultDbSession() as IEFCoreDbSession;
            _dbSession.Attach(this);
        }

        public IEFCoreDbSession DbSession
        {
            get => _dbSession;
            set
            {
                // dbSession 相同直接 return
                if (_dbSession == value) return;
                // dbSession 不同判斷有無設定過，有設定過則拋出 Exception
                if (!_defaultDbSessionIsUsed)
                {
                    throw new InvalidOperationException("DbSession 不能被設定兩次");
                }

                _defaultDbSessionIsUsed = false;
                _dbSession.Detach(this);
                _dbSession = value;
                _dbSession.Attach(this);
            }
        }

        /// <summary>
        /// OnConfiguring 叫用的是 InnerOnConfiguring，具體要每個繼承的實作
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => InnerOnConfiguring(optionsBuilder);

        /// <summary>
        /// <para> 實作設定資料庫連線 </para>
        /// <para> 例子: optionsBuilder.UseMySql(DbSession.GetConnection<MySqlConnection>()) </para>
        /// </summary>
        public abstract void InnerOnConfiguring(DbContextOptionsBuilder optionsBuilder);

        public void UseTransaction(IDbTransaction dbTransaction)
        {
            if (Database.CurrentTransaction != null && dbTransaction != null)
            {
                // 假如 transaction 相同，則不做事，其餘拋出 Exception
                if ((Database.CurrentTransaction as IInfrastructure<IDbTransaction>).Instance == dbTransaction)
                {
                    return;
                }
                throw new InvalidOperationException("已經存在 transaction ，無法再設定 transaction");
            }

            Database.UseTransaction(dbTransaction as DbTransaction);
        }
    }
}