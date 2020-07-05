using System;
using Xunit;

namespace KnstArchitecture.Test.Abstraction
{
    public abstract class XunitKnstArch : IClassFixture<StartupFixture>
    {
        public IServiceProvider ServiceProvider;

        public XunitKnstArch(StartupFixture startupFixture)
        {
            ServiceProvider = startupFixture.ServiceProvider;
        }
    }
}