using System.Data;
using System.Transactions;
using KnstArchitecture.EF.Test;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.DbSessions
{
    public class XunitEFCoreDbSession : XunitKnstArchEF
    {
        [Fact]
        public void BeginTransaction()
        {
            var session = ServiceProvider.GetRequiredService<ITestEFDbSession>();
            var transaction = session.BeginTransaction();

            Assert.Equal(session, transaction);
            Assert.True(transaction.IsTransaction);

            var exception = Assert.Throws<TransactionException>(() => { transaction.BeginTransaction(); });
        }

        [Fact]
        public void SaveChanges()
        {
            var session = ServiceProvider.GetRequiredService<ITestEFDbSession>();
            var dbContext = session.GetCtx<TestContext>();

            Assert.False(dbContext.IsSaveChange);

            session.SaveChanges();

            Assert.True(dbContext.IsSaveChange);
        }

        [Fact]
        public void GetCtx()
        {
            var session = ServiceProvider.GetRequiredService<ITestEFDbSession>();
            var dbContext = session.GetCtx<TestContext>();

            Assert.NotNull(dbContext);
            Assert.Equal(session, dbContext.DbSession);
        }

        [Fact]
        public void GetCtxWithTransaction()
        {
            var session = ServiceProvider.GetRequiredService<ITestEFDbSession>();
            session.BeginTransaction();
            var dbContext = session.GetCtx<TestContext>();

            Assert.NotNull(dbContext);
            Assert.Equal(session, dbContext.DbSession);
            Assert.Equal(session.GetTransaction(), (dbContext.Database.CurrentTransaction as IInfrastructure<IDbTransaction>).Instance);
        }

        [Fact]
        public void GetConnection()
        {
            var session = ServiceProvider.GetRequiredService<ITestEFDbSession>();
            var connection = session.GetConnection();

            Assert.NotNull(connection);
        }

        [Fact]
        public void GetTransaction()
        {
            var session = ServiceProvider.GetRequiredService<ITestEFDbSession>();
            var transaction = session.GetTransaction();

            Assert.Null(transaction);

            session.BeginTransaction();
            transaction = session.GetTransaction();

            Assert.NotNull(transaction);
        }

        [Fact]
        public void GetConnectionT()
        {
            var session = ServiceProvider.GetRequiredService<ITestEFDbSession>();
            var connection = session.GetConnection<IDbConnection>();

            Assert.NotNull(connection);
        }

        [Fact]
        public void GetTransactionT()
        {
            var session = ServiceProvider.GetRequiredService<ITestEFDbSession>();
            var transaction = session.GetTransaction<IDbTransaction>();

            Assert.Null(transaction);

            session.BeginTransaction();
            transaction = session.GetTransaction<IDbTransaction>();

            Assert.NotNull(transaction);
        }

        [Fact]
        public void Commit()
        {
            var session = ServiceProvider.GetRequiredService<ITestEFDbSession>();
            var transaction = session.BeginTransaction();

            transaction.Commit();
            Assert.False(transaction.IsTransaction);
            Assert.Equal(ConnectionState.Closed, transaction.GetConnection().State);
            Assert.Null(transaction.GetTransaction());

            var exception = Assert.Throws<TransactionException>(() => { transaction.Commit(); });
        }

        [Fact]
        public void Rollback()
        {

            var session = ServiceProvider.GetRequiredService<ITestEFDbSession>();
            var transaction = session.BeginTransaction();

            transaction.Rollback();
            Assert.False(transaction.IsTransaction);
            Assert.Equal(ConnectionState.Closed, transaction.GetConnection().State);
            Assert.Null(transaction.GetTransaction());

            var exception = Assert.Throws<TransactionException>(() => { transaction.Rollback(); });
        }
    }
}