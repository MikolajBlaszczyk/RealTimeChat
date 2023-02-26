using RealTimeChat.AccountLogic.Models;

namespace RealTimeChat.AccountLogic.Interfaces;

public interface ISessionHandler
{
    Task<ResponseModel> InitializeSession();
    Task<ResponseModel> TerminateSession();
}
