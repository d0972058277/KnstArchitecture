using KnstArchitecture.Base.Test;
using KnstArchitecture.DbSessions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.DbSessions
{
    public class XunitDbSessionBag : XunitKnstArch
    {
        [Fact]
        public void Add()
        {
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();

            Assert.True(bag.Empty);
            Assert.False(bag.Any);
            Assert.Equal(0, bag.Count);

            var session = ServiceProvider.GetRequiredService<ITestDbSession>();

            Assert.False(bag.Empty);
            Assert.True(bag.Any);
            Assert.Equal(1, bag.Count);
        }

        [Fact]
        public void Remove()
        {
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var session = ServiceProvider.GetRequiredService<ITestDbSession>();
            bag.Remove(session);

            Assert.True(bag.Empty);
            Assert.False(bag.Any);
            Assert.Equal(0, bag.Count);
        }

        [Fact]
        public void Commit()
        {
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var session = ServiceProvider.GetRequiredService<ITestDbSession>();
            bag.Commit();

            Assert.False(session.IsTransaction);
        }

        [Fact]
        public void Rollback()
        {
            var bag = ServiceProvider.GetRequiredService<IDbSessionBag>();
            var session = ServiceProvider.GetRequiredService<ITestDbSession>();
            bag.Rollback();

            Assert.False(session.IsTransaction);
        }
    }
}