using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KnstArchitecture.DbSessions;

namespace KnstArchitecture.Repos.Factories
{
    public class SqlRepoFactory : ISqlRepoFactory
    {
        private bool _disposed = false;
        protected List<ISqlRepo> _waitingTobeDisposedRepo = new List<ISqlRepo>();

        public TRepo Create<TRepo>(Func<ParameterInfo[], object[]> argsFunc, ISqlDbSession dbSession) where TRepo : ISqlRepo
        {
            var iRepoType = typeof(TRepo);

            var repoImpTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => iRepoType.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).ToList();
            if (!repoImpTypes.IsSingle())
            {
                throw new InvalidOperationException($"{iRepoType.FullName}'s implementation class is not single.");
            }

            var repoType = repoImpTypes.Single();
            var ctors = repoType.GetConstructors();
            if (!ctors.IsSingle())
            {
                throw new InvalidOperationException($"{iRepoType.FullName}'s constructor is not single.");
            }

            var ctorParamInfos = ctors.Single().GetParameters();
            var args = argsFunc(ctorParamInfos);

            var repo = (TRepo) Activator.CreateInstance(repoType, args);
            (repo as IRepo).DbSession = dbSession;
            _waitingTobeDisposedRepo.Add(repo);

            return repo;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            foreach (var repo in _waitingTobeDisposedRepo)
            {
                repo.Dispose();
            }
            _waitingTobeDisposedRepo.Clear();
            _waitingTobeDisposedRepo = null;

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}