namespace KnstArchitecture.DbSessions
{
    public interface ISqlServerDbSession : IEFCoreDbSession
    {
        new ISqlServerDbSession BeginTransaction();
    }
}