using System;
using KnstArchitecture.DbSessions;

namespace KnstArchitecture.Repos
{
    public interface IRepo : IDisposable
    {
        IDbSession DbSession { get; set; }
    }
}