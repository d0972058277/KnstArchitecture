using System;
using System.Data;
using KnstArchitecture.Repos;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.Sql.Test
{
    public abstract class XunitKnstArchSql : IDisposable
    {
        public IServiceProvider ServiceProvider;
        public IServiceScope ServiceScope;

        public XunitKnstArchSql()
        {
            var services = new ServiceCollection();
            services.AddKnstArchitectureSql();
            services.AddTransient<IDbConnection>(sp => DbConnectionMoq.MockInterface());

            var serviceProvider = services.BuildServiceProvider();
            ServiceScope = serviceProvider.CreateScope();
            ServiceProvider = ServiceScope.ServiceProvider;
        }

        public void Dispose()
        {
            ServiceScope.Dispose();
        }
    }

    public interface ITestSqlRepo : ISqlRepo { }
    public class TestMySqlRepo : SqlRepo, ITestSqlRepo
    {
        public TestMySqlRepo(ISqlUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}