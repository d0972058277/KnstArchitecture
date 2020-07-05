using System;
using System.Collections.Generic;
using System.Linq;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using KnstArchitecture.Repos.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.UnitOfWorks
{
    public class MultiSqlUnitOfWork : UnitOfWork, IMultiSqlUnitOfWork
    {
        public MultiSqlUnitOfWork(IServiceProvider serviceProvider, IMultiSqlDbSession dbSession) : base(serviceProvider, dbSession) { }

        public override IDbSession CreateDbSession() => (this as IMultiSqlUnitOfWork).CreateDbSession();

        public TRepo Use<TRepo>() where TRepo : IMultiSqlRepo => this.Use<TRepo>(this.GetDefaultDbSession());

        public TRepo Use<TRepo>(IMultiSqlDbSession dbSession) where TRepo : IMultiSqlRepo => base.Create<TRepo>(dbSession);

        IMultiSqlDbSession IMultiSqlUnitOfWork.CreateDbSession() => _serviceProvider.GetRequiredService<IMultiSqlDbSession>();

        public new IMultiSqlDbSession GetDefaultDbSession() => base.GetDefaultDbSession() as IMultiSqlDbSession;

        public virtual TRepo Use<TRepo>(ISqlDbSession dbSession) where TRepo : ISqlRepo
        {
            var sqlRepoFactory = _serviceProvider.GetRequiredService<ISqlRepoFactory>();

            // 這個 Func 主要是設定 Constructor 當中的 Parameters
            // 在 Activator.CreateInstance 時塞入
            var repo = sqlRepoFactory.Create<TRepo>(ctorParamInfos =>
            {
                var args = new List<object>();
                foreach (var param in ctorParamInfos)
                {
                    // new SqlRepo(ISqlUnitOfWork: null);
                    if (param.ParameterType == typeof(ISqlUnitOfWork))
                    {
                        args.Add(default(object));
                    }
                    else
                    {
                        var arg = _serviceProvider.GetService(param.ParameterType);
                        args.Add(arg);
                    }
                }

                return args.ToArray();
            }, dbSession);

            return repo;
        }
    }
}