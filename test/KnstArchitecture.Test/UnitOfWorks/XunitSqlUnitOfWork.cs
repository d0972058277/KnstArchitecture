using KnstArchitecture.DbSessions;
using KnstArchitecture.Sql.Test;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.UnitOfWorks
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

        [Fact]
        public void BeginTransaction()
        {
            var uow = ServiceProvider.GetRequiredService<ISqlUnitOfWork>();
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var defaultSession = uow.GetDefaultDbSession();

            uow.BeginTransaction();

            Assert.True(defaultSession.IsTransaction);
        }

        [Fact]
        public void Commit()
        {
            var uow = ServiceProvider.GetRequiredService<ISqlUnitOfWork>();
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var defaultSession = uow.GetDefaultDbSession();

            uow.BeginTransaction();
            uow.Commit();

            Assert.False(defaultSession.IsTransaction);
        }

        [Fact]
        public void Rollback()
        {
            var uow = ServiceProvider.GetRequiredService<ISqlUnitOfWork>();
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var defaultSession = uow.GetDefaultDbSession();

            uow.BeginTransaction();
            uow.Rollback();

            Assert.False(defaultSession.IsTransaction);
        }

        [Fact]
        public void BeginTransactionWithParameter()
        {
            var uow = ServiceProvider.GetRequiredService<ISqlUnitOfWork>();
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var session = uow.CreateDbSession();

            uow.BeginTransaction(session);

            Assert.True(session.IsTransaction);
        }

        [Fact]
        public void CommitWithParameter()
        {
            var uow = ServiceProvider.GetRequiredService<ISqlUnitOfWork>();
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var session = uow.CreateDbSession();

            uow.BeginTransaction(session);
            uow.Commit(session);

            Assert.False(session.IsTransaction);
        }

        [Fact]
        public void RollbackWithParameter()
        {
            var uow = ServiceProvider.GetRequiredService<ISqlUnitOfWork>();
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var session = uow.CreateDbSession();

            uow.BeginTransaction(session);
            uow.Rollback(session);

            Assert.False(session.IsTransaction);
        }
    }
}