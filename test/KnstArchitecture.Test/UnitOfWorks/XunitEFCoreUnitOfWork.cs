using KnstArchitecture.DbSessions;
using KnstArchitecture.EF.Test;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.UnitOfWorks
{
    public class XunitEFCoreUnitOfWork : XunitKnstArchEF
    {
        [Fact]
        public void CreateDbSession()
        {
            var uow = ServiceProvider.GetRequiredService<IEFCoreUnitOfWork>();
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var defaultSession = uow.GetDefaultDbSession();
            var newSession = uow.CreateDbSession();

            Assert.NotNull(newSession);
            Assert.NotEqual(defaultSession, newSession);
            Assert.Equal(2, bag.Count);
        }

        [Fact]
        public void GetDefaultDbSession()
        {
            var uow = ServiceProvider.GetRequiredService<IEFCoreUnitOfWork>();
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var defaultSession = uow.GetDefaultDbSession();

            Assert.Equal(1, bag.Count);
            Assert.NotNull(defaultSession);
        }

        [Fact]
        public void Use()
        {
            var uow = ServiceProvider.GetRequiredService<IEFCoreUnitOfWork>();
            var defaultSession = uow.GetDefaultDbSession();
            var repo = uow.Use<ITestEFRepo>();

            Assert.NotNull(repo);
            Assert.Equal(defaultSession, repo.DbSession);
        }

        [Fact]
        public void UseWithSession()
        {
            var uow = ServiceProvider.GetRequiredService<IEFCoreUnitOfWork>();
            var session = uow.CreateDbSession();
            var repo = uow.Use<ITestEFRepo>(session);

            Assert.NotNull(repo);
            Assert.Equal(session, repo.DbSession);
        }

        [Fact]
        public void SaveChanges()
        {
            var uow = ServiceProvider.GetRequiredService<IEFCoreUnitOfWork>();
            var dbContext = uow.GetCtx<TestContext>();

            Assert.False(dbContext.IsSaveChange);

            uow.SaveChanges();

            Assert.True(dbContext.IsSaveChange);
        }

        [Fact]
        public void SaveChangesWithSession()
        {
            var uow = ServiceProvider.GetRequiredService<IEFCoreUnitOfWork>();
            var session = uow.CreateDbSession();
            var dbContext = uow.GetCtx<TestContext>(session);

            Assert.False(dbContext.IsSaveChange);
            Assert.False(session.GetCtx<TestContext>().IsSaveChange);

            uow.SaveChanges(session);

            Assert.True(dbContext.IsSaveChange);
            Assert.True(session.GetCtx<TestContext>().IsSaveChange);
        }

        [Fact]
        public void GetCtx()
        {
            var uow = ServiceProvider.GetRequiredService<IEFCoreUnitOfWork>();
            var session = uow.GetDefaultDbSession();
            var dbContext = uow.GetCtx<TestContext>();

            Assert.NotNull(dbContext);
            Assert.Equal(session.GetCtx<TestContext>(), dbContext);
        }

        [Fact]
        public void GetCtxWithSession()
        {
            var uow = ServiceProvider.GetRequiredService<IEFCoreUnitOfWork>();
            var session = uow.CreateDbSession();
            var dbContext = uow.GetCtx<TestContext>(session);

            Assert.NotNull(dbContext);
            Assert.Equal(session.GetCtx<TestContext>(), dbContext);
        }
    }
}