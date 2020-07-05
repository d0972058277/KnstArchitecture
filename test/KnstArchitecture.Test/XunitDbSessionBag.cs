using KnstArchitecture.DbSessions;
using KnstArchitecture.Test.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test
{
    public class XunitDbSessionBag : XunitKnstArch
    {
        public XunitDbSessionBag(StartupFixture startupFixture) : base(startupFixture) { }

        [Fact]
        public void Add()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var bag = sp.GetRequiredService<IDbSessionBag>();

                Assert.True(bag.Empty);
                Assert.False(bag.Any);
                Assert.Equal(0, bag.Count);

                var session = sp.GetRequiredService<ITestDbSession>();

                Assert.False(bag.Empty);
                Assert.True(bag.Any);
                Assert.Equal(1, bag.Count);
            }
        }

        [Fact]
        public void Remove()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var bag = sp.GetRequiredService<IDbSessionBag>();
                var session = sp.GetRequiredService<ITestDbSession>();
                bag.Remove(session);

                Assert.True(bag.Empty);
                Assert.False(bag.Any);
                Assert.Equal(0, bag.Count);
            }
        }

        [Fact]
        public void Commit()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var bag = sp.GetRequiredService<IDbSessionBag>();
                var session = sp.GetRequiredService<ITestDbSession>();
                bag.Commit();

                Assert.False(session.IsTransaction);
            }
        }

        [Fact]
        public void Rollback()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var bag = sp.GetRequiredService<IDbSessionBag>();
                var session = sp.GetRequiredService<ITestDbSession>();
                bag.Rollback();

                Assert.False(session.IsTransaction);
            }
        }
    }
}