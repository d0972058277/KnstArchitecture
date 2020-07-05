using KnstArchitecture.Test.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test
{
    public class XunitRepo : XunitKnstArch
    {
        public XunitRepo(StartupFixture startupFixture) : base(startupFixture) { }

        [Fact]
        public void DbSession()
        {
            using(var scope = ServiceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var uow = sp.GetRequiredService<ITestUnitOfWork>();
                var repo = sp.GetRequiredService<ITestRepo>();

                Assert.Equal(uow.GetDefaultDbSession(), repo.DbSession);
            }
        }
    }
}