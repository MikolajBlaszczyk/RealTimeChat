namespace RealTimeChat.AccountLogic.Interfaces;

public interface ISessionHandler
{
    Task InitializeSession();
    Task TerminateSession();
}