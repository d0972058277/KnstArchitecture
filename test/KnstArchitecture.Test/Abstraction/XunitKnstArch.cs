using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.Abstraction
{
    public abstract class XunitKnstArch : IDisposable
    {
        public IServiceProvider ServiceProvider;
        public IServiceScope ServiceScope;

        public XunitKnstArch()
        {
            var services = new ServiceCollection();
            services.TryAddKnstArchitecture();
            services.AddTransient<ITestDbSession, TestDbSession>();
            services.AddScoped<ITestUnitOfWork, TestUnitOfWork>();

            var serviceProvider = services.BuildServiceProvider();
            ServiceScope = serviceProvider.CreateScope();
            ServiceProvider = ServiceScope.ServiceProvider;
        }

        public void Dispose()
        {
            ServiceScope.Dispose();
        }
    }

    public interface ITestDbSession : IDbSession { }
    public class TestDbSession : DbSession, ITestDbSession
    {
        public TestDbSession(IDbSessionBag dbSessionBag) : base(dbSessionBag) { }
    }

    public interface ITestUnitOfWork : IUnitOfWork { }
    public class TestUnitOfWork : UnitOfWork, ITestUnitOfWork
    {
        public TestUnitOfWork(IServiceProvider serviceProvider, ITestDbSession dbSession) : base(serviceProvider, dbSession) { }

        public override IDbSession CreateDbSession() => _serviceProvider.GetRequiredService<ITestDbSession>();
    }

    public interface ITestRepo : IRepo { }
    public class TestRepo : Repo, ITestRepo
    {
        public TestRepo(ITestUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}