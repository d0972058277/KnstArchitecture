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
            //Given

            //When

            //Then
        }

        [Fact]
        public void RemoveDefaultFilter()
        {
            //Given

            //When

            //Then
        }

        [Fact]
        public void SetDefaultFilter()
        {
            //Given

            //When

            //Then
        }

        [Fact]
        public void Default()
        {
            //Given

            //When

            //Then
        }

        [Fact]
        public void First()
        {
            //Given

            //When

            //Then
        }

        [Fact]
        public void Last()
        {
            //Given

            //When

            //Then
        }

        [Fact]
        public void Index()
        {
            //Given

            //When

            //Then
        }
    }
}