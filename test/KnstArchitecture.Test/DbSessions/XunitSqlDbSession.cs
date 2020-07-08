using System.Data;
using System.Transactions;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Sql.Test;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.DbSessions
{
    public class XunitSqlDbSession : XunitKnstArchSql
    {
        [Fact]
        public void BeginTransaction()
        {
            var session = ServiceProvider.GetRequiredService<ISqlDbSession>();
            var transaction = session.BeginTransaction();

            Assert.Equal(session, transaction);
            Assert.True(transaction.IsTransaction);
            Assert.Equal(ConnectionState.Open, transaction.GetTransaction().Connection.State);
            Assert.NotNull(transaction.GetTransaction());

            var exception = Assert.Throws<TransactionException>(() => { transaction.BeginTransaction(); });
        }

        [Fact]
        public void GetConnection()
        {
            var session = ServiceProvider.GetRequiredService<ISqlDbSession>();
            var connection = session.GetConnection();

            Assert.NotNull(connection);
        }

        [Fact]
        public void GetTransaction()
        {
            var session = ServiceProvider.GetRequiredService<ISqlDbSession>();
            var transaction = session.GetTransaction();

            Assert.Null(transaction);

            session.BeginTransaction();
            transaction = session.GetTransaction();

            Assert.NotNull(transaction);
        }

        [Fact]
        public void GetConnectionT()
        {
            var session = ServiceProvider.GetRequiredService<ISqlDbSession>();
            var connection = session.GetConnection<IDbConnection>();

            Assert.NotNull(connection);
        }

        [Fact]
        public void GetTransactionT()
        {
            var session = ServiceProvider.GetRequiredService<ISqlDbSession>();
            var transaction = session.GetTransaction<IDbTransaction>();

            Assert.Null(transaction);

            session.BeginTransaction();
            transaction = session.GetTransaction<IDbTransaction>();

            Assert.NotNull(transaction);
        }

        [Fact]
        public void Commit()
        {
            var session = ServiceProvider.GetRequiredService<ISqlDbSession>();
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

            var session = ServiceProvider.GetRequiredService<ISqlDbSession>();
            var transaction = session.BeginTransaction();

            transaction.Rollback();
            Assert.False(transaction.IsTransaction);
            Assert.Equal(ConnectionState.Closed, transaction.GetConnection().State);
            Assert.Null(transaction.GetTransaction());

            var exception = Assert.Throws<TransactionException>(() => { transaction.Rollback(); });
        }
    }
}