using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.Abstraction
{
    public abstract class XunitKnstArchSql : IDisposable
    {
        public IServiceProvider ServiceProvider;
        public IServiceScope ServiceScope;

        public XunitKnstArchSql()
        {
            var services = new ServiceCollection();
            services.AddKnstArchitectureSql();

            var serviceProvider = services.BuildServiceProvider();
            ServiceScope = serviceProvider.CreateScope();
            ServiceProvider = ServiceScope.ServiceProvider;
        }

        public void Dispose()
        {
            ServiceScope.Dispose();
        }
    }

    public interface ITestMySqlRepo : IRepo { }
    public class TestMySqlRepo : SqlRepo, ITestMySqlRepo
    {
        public TestMySqlRepo(ISqlUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}