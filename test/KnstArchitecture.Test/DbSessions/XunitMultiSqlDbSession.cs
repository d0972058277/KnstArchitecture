using System.Linq;
using KnstArchitecture.DbSessions;
using KnstArchitecture.Test.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace KnstArchitecture.Test.DbSessions
{
    public class XunitMultiSqlDbSession : XunitKnstArchMulti
    {
        [Fact]
        public void SqlDbSessions()
        {
            var session = ServiceProvider.GetRequiredService<IMultiSqlDbSession>();
            var sessionCount = session.SqlDbSessions.Count();

            Assert.NotEmpty(session.SqlDbSessions);
            Assert.Equal(2, sessionCount);
        }

        [Fact]
        public void BeginTransaction()
        {
            var session = ServiceProvider.GetRequiredService<IMultiSqlDbSession>();
            var transaction = session.BeginTransaction();

            Assert.Equal(session, transaction);
            Assert.True(transaction.IsTransaction);
            Assert.All(session.SqlDbSessions, s => Assert.True(s.IsTransaction));
        }

        [Fact]
        public void RemoveDefaultFilter()
        {
            var session = ServiceProvider.GetRequiredService<IMultiSqlDbSession>();
            session.SetDefaultFilter(sessions => sessions.First());

            Assert.NotNull(session.DefaultFilter);

            session.RemoveDefaultFilter();

            Assert.Null(session.DefaultFilter);
        }

        [Fact]
        public void SetDefaultFilter()
        {
            var session = ServiceProvider.GetRequiredService<IMultiSqlDbSession>();
            session.SetDefaultFilter(sessions => sessions.First());

            Assert.NotNull(session.DefaultFilter);
        }

        [Fact]
        public void Default()
        {
            var session = ServiceProvider.GetRequiredService<IMultiSqlDbSession>();
            session.SetDefaultFilter(sessions => sessions.First());
            var defaultSession = session.Default();

            Assert.Equal(session.SqlDbSessions.First(), defaultSession);
        }

        [Fact]
        public void First()
        {
            var session = ServiceProvider.GetRequiredService<IMultiSqlDbSession>();

            Assert.Equal(session.SqlDbSessions.First(), session.First());
        }

        [Fact]
        public void Last()
        {
            var session = ServiceProvider.GetRequiredService<IMultiSqlDbSession>();

            Assert.Equal(session.SqlDbSessions.Last(), session.Last());
        }

        [Fact]
        public void Index()
        {
            var session = ServiceProvider.GetRequiredService<IMultiSqlDbSession>();

            Assert.Equal(session.SqlDbSessions.First(), session[0]);
            Assert.Equal(session.SqlDbSessions.Last(), session[1]);
        }
    }
}