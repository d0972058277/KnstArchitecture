using System.Data;
using System.Data.Common;
using Moq;

namespace KnstArchitecture.EF.Test
{
    public static class DbConnectionMoq
    {
        public static IDbConnection MockInterface()
        {
            var transactionMock = new Mock<DbTransaction>();
            transactionMock.Setup(t => t.Commit());
            transactionMock.Setup(t => t.Rollback());

            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.Open());
            connectionMock.Setup(c => c.BeginTransaction()).Returns(transactionMock.Object);

            return connectionMock.Object;
        }
    }
}