using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Repos;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.Test
{
    public class StartupFixture : IDisposable
    {
        public StartupFixture()
        {
            var services = new ServiceCollection();
            services.TryAddKnstArchitecture();
            services.AddTransient<ITestDbSession, TestDbSession>();
            services.AddScoped<ITestUnitOfWork, TestUnitOfWork>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public IServiceProvider ServiceProvider { get; }

        public void Dispose() { }
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