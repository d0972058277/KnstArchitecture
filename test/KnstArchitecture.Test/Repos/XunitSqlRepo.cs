using KnstArchitecture.Sql.Test;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.Repos
{
    public class XunitSqlRepo : XunitKnstArchSql
    {
        [Fact]
        public void Connection()
        {
            var uow = ServiceProvider.GetRequiredService<ISqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestSqlRepo>();
            var defaultSession = uow.GetDefaultDbSession();

            Assert.Equal(defaultSession.GetConnection(), repo.Connection);
        }

        [Fact]
        public void Transaction()
        {
            var uow = ServiceProvider.GetRequiredService<ISqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestSqlRepo>();
            var defaultSession = uow.GetDefaultDbSession();

            Assert.Equal(defaultSession.GetTransaction(), repo.Transaction);
        }
    }
}