namespace KnstArchitecture.DbSessions
{
    public interface IMySqlDbSession : IEFCoreDbSession
    {
        new IMySqlDbSession BeginTransaction();
    }
}