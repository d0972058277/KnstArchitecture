using KnstArchitecture.Test.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.Repos
{
    public class XunitRepo : XunitKnstArch
    {
        [Fact]
        public void DbSession()
        {
            var uow = ServiceProvider.GetRequiredService<ITestUnitOfWork>();
            var repo = ServiceProvider.GetRequiredService<ITestRepo>();

            Assert.Equal(uow.GetDefaultDbSession(), repo.DbSession);
        }
    }
}