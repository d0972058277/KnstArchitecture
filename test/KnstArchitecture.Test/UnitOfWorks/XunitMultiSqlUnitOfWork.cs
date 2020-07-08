using KnstArchitecture.DbSessions;
using KnstArchitecture.Test.Abstractions;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.UnitOfWorks
{
    public class XunitMultiSqlUnitOfWork : XunitKnstArchMulti
    {
        [Fact]
        public void CreateDbSession()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var defaultSession = uow.GetDefaultDbSession();
            var newSession = uow.CreateDbSession();

            Assert.NotNull(newSession);
            Assert.NotEqual(defaultSession, newSession);
            // multi1 { sql1, sql2 }
            // multi2 { sql3, sql4 }
            Assert.Equal(6, bag.Count);
        }

        [Fact]
        public void GetDefaultDbSession()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var defaultSession = uow.GetDefaultDbSession();

            // multi1 { sql1, sql2 }
            Assert.Equal(3, bag.Count);
            Assert.NotNull(defaultSession);
        }

        [Fact]
        public void Use()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var defaultSession = uow.GetDefaultDbSession();
            var repo = uow.Use<ITestMultiSqlRepo>();

            Assert.NotNull(repo);
            Assert.Equal(defaultSession, repo.DbSession);
        }

        [Fact]
        public void UseWithSqlDbSession()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var defaultSession = uow.GetDefaultDbSession();
            var firstSession = defaultSession.First();
            var repo = uow.Use<Sql.Test.ITestSqlRepo>(firstSession);

            Assert.NotNull(repo);
            Assert.Equal(firstSession, repo.DbSession);
        }

        [Fact]
        public void UseWithMultiSqlDbSession()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var session = uow.CreateDbSession();
            var repo = uow.Use<ITestMultiSqlRepo>(session);

            Assert.NotNull(repo);
            Assert.Equal(session, repo.DbSession);
        }
    }
}