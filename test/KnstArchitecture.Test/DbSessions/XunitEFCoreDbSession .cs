using System.Transactions;
using KnstArchitecture.EF.Test;
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
    }
}