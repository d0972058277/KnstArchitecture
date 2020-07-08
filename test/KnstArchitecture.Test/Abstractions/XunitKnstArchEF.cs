using System;
using System.Data;
using System.Data.Common;
using KnstArchitecture.DbSessions;
using KnstArchitecture.EF.DbContexts;
using KnstArchitecture.Repos;
using KnstArchitecture.Test;
using KnstArchitecture.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace KnstArchitecture.EF.Test
{
    public abstract class XunitKnstArchEF : IDisposable
    {
        public IServiceProvider ServiceProvider;
        public IServiceScope ServiceScope;

        public XunitKnstArchEF()
        {
            var services = new ServiceCollection();
            services.TryAddKnstArchitecture();
            services.AddTransient<ITestEFDbSession, TestEFDbSession>();
            services.AddTransient<IEFCoreDbSession>(sp => sp.GetRequiredService<ITestEFDbSession>());
            services.AddScoped<ITestEFUnitOfWork, TestEFUnitOfWork>();
            services.AddScoped<IEFCoreUnitOfWork>(sp => sp.GetRequiredService<ITestEFUnitOfWork>());
            services.TryAddKnstDbContexts();
            services.AddTransient<IDbConnection>(sp => DbConnectionMoq.GetMemorySqlite());

            var serviceProvider = services.BuildServiceProvider();
            ServiceScope = serviceProvider.CreateScope();
            ServiceProvider = ServiceScope.ServiceProvider;
        }

        public void Dispose()
        {
            ServiceScope.Dispose();
        }
    }

    public interface ITestEFDbSession : IEFCoreDbSession { }
    public class TestEFDbSession : EFCoreDbSession, ITestEFDbSession
    {
        public TestEFDbSession(IDbSessionBag dbSessionBag, IDbConnection connection, IServiceProvider serviceProvider) : base(dbSessionBag, connection, serviceProvider) { }
    }

    public interface ITestEFUnitOfWork : IEFCoreUnitOfWork { }
    public class TestEFUnitOfWork : EFCoreUnitOfWork, ITestEFUnitOfWork
    {
        public TestEFUnitOfWork(IServiceProvider serviceProvider, ITestEFDbSession sqlDbSession) : base(serviceProvider, sqlDbSession) { }
    }

    public interface ITestEFRepo : IEFCoreRepo { }
    public class TestEFRepo : EFCoreRepo, ITestEFRepo
    {
        public TestEFRepo(IEFCoreUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public interface ITestEFCtxRepo : IEFCoreRepo<TestContext> { }
    public class TestEFCtxRepo : EFCoreRepo<TestContext>, ITestEFCtxRepo
    {
        public TestEFCtxRepo(IEFCoreUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public class TestContext : KnstDbContext
    {
        public bool IsSaveChange { get; private set; } = false;

        public TestContext(IEFCoreUnitOfWork efUnitOfWork) : base(efUnitOfWork) { }

        public override void InnerOnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(DbSession.GetConnection() as DbConnection);
        }

        public override int SaveChanges()
        {
            IsSaveChange = true;
            return 0;
        }
    }
}