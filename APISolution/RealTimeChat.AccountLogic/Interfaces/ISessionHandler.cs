namespace RealTimeChat.AccountLogic.Interfaces;

public interface ISessionHandler
{
    Task InitializeSession(IUserModel user);
    Task TerminateSession(IUserModel user);
    Task TerminateAllSessions();
}