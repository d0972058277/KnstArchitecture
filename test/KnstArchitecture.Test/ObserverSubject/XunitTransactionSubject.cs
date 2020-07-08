using System.Data;
using KnstArchitecture.EF.Test;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.ObserverSubject
{
    public class XunitTransactionSubject : XunitKnstArchEF
    {
        [Fact]
        public void Attach()
        {
            var uow = ServiceProvider.GetRequiredService<ITestEFUnitOfWork>();
            var dbContext = ServiceProvider.GetRequiredService<TestContext>();
            var session = uow.CreateDbSession();

            Assert.Empty(session.Observers);

            session.Attach(dbContext);

            Assert.Single(session.Observers);
        }

        [Fact]
        public void Detach()
        {
            var uow = ServiceProvider.GetRequiredService<ITestEFUnitOfWork>();
            var dbContext = ServiceProvider.GetRequiredService<TestContext>();
            var session = uow.CreateDbSession();
            session.Attach(dbContext);

            Assert.Single(session.Observers);

            session.Detach(dbContext);

            Assert.Empty(session.Observers);
        }

        [Fact]
        public void NotifyObserverUseTransaction()
        {
            var uow = ServiceProvider.GetRequiredService<ITestEFUnitOfWork>();
            var dbContext = ServiceProvider.GetRequiredService<TestContext>();
            var session = uow.CreateDbSession();
            session.BeginTransaction();

            dbContext.DbSession = session;
            session.NotifyObserversUseTransaction();

            // ref: https://dapper-tutorial.net/zh-TW/knowledge-base/46566756/
            Assert.Equal(session.GetTransaction(), (dbContext.Database.CurrentTransaction as IInfrastructure<IDbTransaction>).Instance);
        }
    }
}