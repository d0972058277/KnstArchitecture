namespace KnstArchitecture.DbSessions
{
    public interface ISqliteDbSession : IEFCoreDbSession
    {
        new ISqliteDbSession BeginTransaction();
    }
}