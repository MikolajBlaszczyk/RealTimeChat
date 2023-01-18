using RealTimeChat.BusinessLogic.AccountLogic.Interfaces;

namespace RealTimeChat.BusinessLogic.AccountLogic.SessionManager;

public interface ISessionHandler
{
    Task InitializeSession(IUserModel user);
    Task TerminateSession(IUserModel user);
    Task TerminateAllSessions();
}