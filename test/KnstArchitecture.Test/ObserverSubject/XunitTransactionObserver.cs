using System.Data;
using KnstArchitecture.EF.Test;
using KnstArchitecture.UnitOfWorks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.ObserverSubject
{
    public class XunitTransactionObserver : XunitKnstArchEF
    {
        [Fact]
        public void UseTransaction()
        {
            var uow = ServiceProvider.GetRequiredService<IEFCoreUnitOfWork>();
            var dbContext = uow.GetCtx<TestContext>();
            var session = uow.GetDefaultDbSession();
            uow.BeginTransaction();
            Assert.NotNull(dbContext.Database.CurrentTransaction);

            // ref: https://dapper-tutorial.net/zh-TW/knowledge-base/46566756/
            Assert.Equal(session.GetTransaction(), (dbContext.Database.CurrentTransaction as IInfrastructure<IDbTransaction>).Instance);
        }
    }
}