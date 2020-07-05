using System;
using System.Data;

namespace KnstArchitecture.Queries
{
    public interface IQuery : IDisposable
    {
        IDbConnection Connection { get; }
    }
}