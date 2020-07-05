using System.Transactions;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Test.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test
{
    public class XunitUnitOfWork : XunitKnstArch
    {
        public XunitUnitOfWork(StartupFixture startupFixture) : base(startupFixture) { }

        [Fact]
        public void GetDefaultDbSession()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var uow = sp.GetRequiredService<ITestUnitOfWork>();
                var bag = sp.GetRequiredService<IDbSessionBag>();
                var defaultSession = uow.GetDefaultDbSession();

                Assert.Equal(1, bag.Count);
                Assert.NotNull(defaultSession);
            }
        }

        [Fact]
        public void CreateDbSession()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var uow = sp.GetRequiredService<ITestUnitOfWork>();
                var bag = sp.GetRequiredService<IDbSessionBag>();
                var defaultSession = uow.GetDefaultDbSession();
                var newSession = uow.CreateDbSession();

                Assert.NotNull(newSession);
                Assert.NotEqual(defaultSession, newSession);
                Assert.Equal(2, bag.Count);
            }
        }

        [Fact]
        public void BeginTransaction()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var uow = sp.GetRequiredService<ITestUnitOfWork>();
                var bag = sp.GetRequiredService<IDbSessionBag>();
                var defaultSession = uow.GetDefaultDbSession();

                uow.BeginTransaction();

                Assert.True(defaultSession.IsTransaction);
            }
        }

        [Fact]
        public void Commit()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var uow = sp.GetRequiredService<ITestUnitOfWork>();
                var bag = sp.GetRequiredService<IDbSessionBag>();
                var defaultSession = uow.GetDefaultDbSession();

                uow.BeginTransaction();
                uow.Commit();

                Assert.False(defaultSession.IsTransaction);
            }
        }

        [Fact]
        public void Rollback()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var uow = sp.GetRequiredService<ITestUnitOfWork>();
                var bag = sp.GetRequiredService<IDbSessionBag>();
                var defaultSession = uow.GetDefaultDbSession();

                uow.BeginTransaction();
                uow.Rollback();

                Assert.False(defaultSession.IsTransaction);
            }
        }

                [Fact]
        public void BeginTransactionWithParameter()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var uow = sp.GetRequiredService<ITestUnitOfWork>();
                var bag = sp.GetRequiredService<IDbSessionBag>();
                var session = uow.CreateDbSession();

                uow.BeginTransaction(session);

                Assert.True(session.IsTransaction);
            }
        }

        [Fact]
        public void CommitWithParameter()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var uow = sp.GetRequiredService<ITestUnitOfWork>();
                var bag = sp.GetRequiredService<IDbSessionBag>();
                var session = uow.CreateDbSession();

                uow.BeginTransaction(session);
                uow.Commit(session);

                Assert.False(session.IsTransaction);
            }
        }

        [Fact]
        public void RollbackWithParameter()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var uow = sp.GetRequiredService<ITestUnitOfWork>();
                var bag = sp.GetRequiredService<IDbSessionBag>();
                var session = uow.CreateDbSession();

                uow.BeginTransaction(session);
                uow.Rollback(session);

                Assert.False(session.IsTransaction);
            }
        }
    }
}