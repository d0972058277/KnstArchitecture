using System.Data;
using Moq;
using MySql.Data.MySqlClient;

namespace KnstArchitecture.Test.Mocks
{
    public static class DbConnectionMoq
    {
        public static IDbConnection MockInterface()
        {
            var transactionMock = new Mock<IDbTransaction>();
            transactionMock.Setup(t => t.Commit());
            transactionMock.Setup(t => t.Rollback());

            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.Open());
            connectionMock.Setup(c => c.BeginTransaction()).Returns(transactionMock.Object);

            return connectionMock.Object;
        }

        public static MySqlConnection MockMySql()
        {
            var transactionMock = new Mock<MySqlTransaction>();
            transactionMock.Setup(t => t.Commit());
            transactionMock.Setup(t => t.Rollback());

            var connectionMock = new Mock<MySqlConnection>();
            connectionMock.Setup(c => c.Open());
            connectionMock.Setup(c => c.BeginTransaction()).Returns(transactionMock.Object);

            return connectionMock.Object;
        }
    }
}