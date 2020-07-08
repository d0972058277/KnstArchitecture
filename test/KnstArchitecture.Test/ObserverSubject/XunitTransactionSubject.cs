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

            Assert.Empty(session.ReadonlyKnstDbContext);

            session.Attach(dbContext);

            Assert.Single(session.ReadonlyKnstDbContext);
        }

        [Fact]
        public void Detach()
        {
            var uow = ServiceProvider.GetRequiredService<ITestEFUnitOfWork>();
            var dbContext = ServiceProvider.GetRequiredService<TestContext>();
            var session = uow.CreateDbSession();
            session.Attach(dbContext);

            Assert.Single(session.ReadonlyKnstDbContext);

            session.Detach(dbContext);

            Assert.Empty(session.ReadonlyKnstDbContext);
        }

        [Fact]
        public void NotifyObserverUseTransaction()
        {
            var uow = ServiceProvider.GetRequiredService<ITestEFUnitOfWork>();
            var dbContext = ServiceProvider.GetRequiredService<TestContext>();
            var session = uow.CreateDbSession();
            session.BeginTransaction();

            dbContext.DbSession = session;
            session.NotifyObserverUseTransaction();

            // ref: https://dapper-tutorial.net/zh-TW/knowledge-base/46566756/
            Assert.Equal(session.GetTransaction(), (dbContext.Database.CurrentTransaction as IInfrastructure<IDbTransaction>).Instance);
        }
    }
}