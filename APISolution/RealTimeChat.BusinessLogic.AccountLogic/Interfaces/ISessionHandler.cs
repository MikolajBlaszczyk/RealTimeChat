using RealTimeChat.BusinessLogic.AccountLogic.Interfaces;

namespace RealTimeChat.BusinessLogic.AccountLogic.SessionManager;

public interface ISessionHandler: IDisposable
{
    Task InitializeSession(IUserModel user);
    Task TerminateSession(string userName);
    Task TerminateAllSessions();
}