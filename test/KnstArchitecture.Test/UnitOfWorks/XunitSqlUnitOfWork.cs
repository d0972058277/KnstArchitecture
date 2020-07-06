using KnstArchitecture.DbSessions;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Sql.Test
{
    public class XunitSqlUnitOfWork : XunitKnstArchSql
    {
        [Fact]
        public void CreateDbSession()
        {
            var uow = ServiceProvider.GetRequiredService<ISqlUnitOfWork>();
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
            var uow = ServiceProvider.GetRequiredService<ISqlUnitOfWork>();
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var defaultSession = uow.GetDefaultDbSession();

            Assert.Equal(1, bag.Count);
            Assert.NotNull(defaultSession);
        }

        [Fact]
        public void Use()
        {
            var uow = ServiceProvider.GetRequiredService<ISqlUnitOfWork>();
            var defaultSession = uow.GetDefaultDbSession();
            var repo = uow.Use<ITestSqlRepo>();

            Assert.NotNull(repo);
            Assert.Equal(defaultSession, repo.DbSession);
        }

        [Fact]
        public void UseWithSession()
        {
            var uow = ServiceProvider.GetRequiredService<ISqlUnitOfWork>();
            var session = uow.CreateDbSession();
            var repo = uow.Use<ITestSqlRepo>(session);

            Assert.NotNull(repo);
            Assert.Equal(session, repo.DbSession);
        }
    }
}