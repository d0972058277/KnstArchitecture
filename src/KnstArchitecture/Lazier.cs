using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnstArchitecture
{
    internal class Lazier<T> : Lazy<T>
    {
        public Lazier(IServiceProvider provider) : base(() => provider.GetRequiredService<T>()) { }
    }
}
