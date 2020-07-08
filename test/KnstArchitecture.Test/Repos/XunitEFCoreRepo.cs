using KnstArchitecture.EF.Test;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.Repos
{
    public class XunitEFCoreRepo : XunitKnstArchEF
    {
        [Fact]
        public void DbSession()
        {
            var uow = ServiceProvider.GetRequiredService<ITestEFUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestEFRepo>();

            Assert.Equal(uow.GetDefaultDbSession(), repo.DbSession);
        }

        [Fact]
        public void Connection()
        {
            var uow = ServiceProvider.GetRequiredService<ITestEFUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestEFRepo>();
            var defaultSession = uow.GetDefaultDbSession();

            Assert.Equal(defaultSession.GetConnection(), repo.Connection);
        }

        [Fact]
        public void Transaction()
        {
            var uow = ServiceProvider.GetRequiredService<ITestEFUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestEFRepo>();
            var defaultSession = uow.GetDefaultDbSession();

            Assert.Equal(defaultSession.GetTransaction(), repo.Transaction);
        }

        [Fact]
        public void DbContext()
        {
            var uow = ServiceProvider.GetRequiredService<ITestEFUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestEFCtxRepo>();
            var defaultSession = uow.GetDefaultDbSession();
            var dbContext = repo.DbContext;

            Assert.Equal(defaultSession.GetCtx<TestContext>(), dbContext);
        }
    }
}