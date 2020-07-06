using System;
using System.Transactions;
using KnstArchitecture.Test.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.DbSessions
{
    public class XunitDbSession : XunitKnstArch
    {
        [Fact]
        public void BeginTransaction()
        {
            var session = ServiceProvider.GetRequiredService<ITestDbSession>();
            var transaction = session.BeginTransaction();

            Assert.Equal(session, transaction);
            Assert.True(transaction.IsTransaction);

            var exception = Assert.Throws<TransactionException>(() => { transaction.BeginTransaction(); });
        }

        [Fact]
        public void Commit()
        {
            var session = ServiceProvider.GetRequiredService<ITestDbSession>();
            var transaction = session.BeginTransaction();

            transaction.Commit();
            Assert.False(transaction.IsTransaction);

            var exception = Assert.Throws<TransactionException>(() => { transaction.Commit(); });
        }

        [Fact]
        public void Rollback()
        {
            var session = ServiceProvider.GetRequiredService<ITestDbSession>();
            var transaction = session.BeginTransaction();

            transaction.Rollback();
            Assert.False(transaction.IsTransaction);

            var exception = Assert.Throws<TransactionException>(() => { transaction.Rollback(); });
        }
    }
}