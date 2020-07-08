using System.Linq;
using KnstArchitecture.Multi.Test;
using KnstArchitecture.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.Repos
{
    public class XunitMultiSqlRepo : XunitKnstArchMulti
    {
        [Fact]
        public void DbSession()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestMultiSqlRepo>();
            var defaultSession = uow.GetDefaultDbSession();

            Assert.Equal(defaultSession, repo.DbSession);
        }

        [Fact]
        public void Connections()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestMultiSqlRepo>();
            var defaultSession = uow.GetDefaultDbSession();

            Assert.NotEmpty(repo.Connections);
            Assert.All(repo.Connections, conns => Assert.NotNull(conns));

            var sqlSessionCount = defaultSession.SqlDbSessions.Count();
            var connectionCount = repo.Connections.Count();

            Assert.Equal(sqlSessionCount, connectionCount);
        }

        [Fact]
        public void Transactions()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestMultiSqlRepo>();
            var defaultSession = uow.GetDefaultDbSession();

            Assert.All(repo.Transactions, trans => Assert.Null(trans));

            defaultSession.BeginTransaction();

            Assert.All(repo.Transactions, trans => Assert.NotNull(trans));
        }

        [Fact]
        public void DefaultConnection()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestMultiSqlRepo>();
            uow.GetDefaultDbSession().SetDefaultFilter(sessions => sessions.First());

            Assert.Equal(uow.GetDefaultDbSession().Default().GetConnection(), repo.DefaultConnection);
        }

        [Fact]
        public void DefaultTransaction()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestMultiSqlRepo>();
            uow.GetDefaultDbSession().SetDefaultFilter(sessions => sessions.First());
            uow.BeginTransaction();

            Assert.NotNull(repo.DefaultTransaction);
            Assert.Equal(uow.GetDefaultDbSession().Default().GetTransaction(), repo.DefaultTransaction);
        }

        [Fact]
        public void DefaultFilter()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestMultiSqlRepo>();
            repo.SetDefaultFilter(sessions => sessions.First());

            Assert.Equal(uow.GetDefaultDbSession().DefaultFilter, repo.DefaultFilter);
        }

        [Fact]
        public void RemoveDefaultFilter()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestMultiSqlRepo>();
            repo.SetDefaultFilter(sessions => sessions.First());
            repo.RemoveDefaultFilter();

            Assert.Null(repo.DefaultFilter);
            Assert.Null(uow.GetDefaultDbSession().DefaultFilter);
        }

        [Fact]
        public void SetDefaultFilter()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestMultiSqlRepo>();
            repo.SetDefaultFilter(sessions => sessions.First());

            Assert.NotNull(repo.DefaultFilter);
            Assert.Equal(uow.GetDefaultDbSession().DefaultFilter, repo.DefaultFilter);
        }

        [Fact]
        public void Default()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestMultiSqlRepo>();
            repo.SetDefaultFilter(sessions => sessions.First());

            Assert.Equal(uow.GetDefaultDbSession().Default(), repo.Default());
        }

        [Fact]
        public void First()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestMultiSqlRepo>();

            Assert.Equal(uow.GetDefaultDbSession().First(), repo.First());
        }

        [Fact]
        public void Last()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestMultiSqlRepo>();

            Assert.Equal(uow.GetDefaultDbSession().Last(), repo.Last());
        }

        [Fact]
        public void Index()
        {
            var uow = ServiceProvider.GetRequiredService<IMultiSqlUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestMultiSqlRepo>();
            repo.SetDefaultFilter(sessions => sessions.First());

            Assert.Equal(uow.GetDefaultDbSession() [0], repo[0]);
        }
    }
}