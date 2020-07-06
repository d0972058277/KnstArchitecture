using System;
using System.Data;
using System.Transactions;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Test.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
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

            Assert.Throws<InvalidCastException>(() => session.GetConnection<MySqlConnection>());
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

            Assert.Throws<InvalidCastException>(() => session.GetTransaction<MySqlTransaction>());
        }
    }
}