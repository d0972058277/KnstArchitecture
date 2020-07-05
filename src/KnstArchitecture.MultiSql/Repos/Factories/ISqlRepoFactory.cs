using System;
using System.Reflection;
using KnstArchitecture.DbSessions;

namespace KnstArchitecture.Repos.Factories
{
    public interface ISqlRepoFactory : IDisposable
    {
        TRepo Create<TRepo>(Func<ParameterInfo[], object[]> argsFunc, ISqlDbSession dbSession) where TRepo : ISqlRepo;
    }
}