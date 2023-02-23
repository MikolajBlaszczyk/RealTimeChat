using RealTimeChat.AccountLogic.Interfaces;

namespace RealTimeChat.AccountLogic.SessionManager;

public interface ISessionHandler: IDisposable
{
    Task InitializeSession(IUserModel user);
    Task TerminateSession(string userName);
    Task TerminateAllSessions();
}