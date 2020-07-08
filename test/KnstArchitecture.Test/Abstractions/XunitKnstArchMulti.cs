using System;
using System.Data;
using KnstArchitecture.Repos;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.Test.Abstractions
{
    public class XunitKnstArchMulti : IDisposable
    {
        public IServiceProvider ServiceProvider;
        public IServiceScope ServiceScope;

        public XunitKnstArchMulti()
        {
            var services = new ServiceCollection();
            services.AddTransient<IDbConnection>(sp => DbConnectionMoq.GetMemorySqlite());
            services.AddTransient<IDbConnection>(sp => DbConnectionMoq.GetMemorySqlite());
            services.AddKnstArchitectureMultiSql();

            var serviceProvider = services.BuildServiceProvider();
            ServiceScope = serviceProvider.CreateScope();
            ServiceProvider = ServiceScope.ServiceProvider;
        }

        public void Dispose()
        {
            ServiceScope?.Dispose();
        }
    }

    public interface ITestMultiSqlRepo : IMultiSqlRepo { }
    public class TestSqlRepo : MultiSqlRepo, ITestMultiSqlRepo
    {
        public TestSqlRepo(IMultiSqlUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}