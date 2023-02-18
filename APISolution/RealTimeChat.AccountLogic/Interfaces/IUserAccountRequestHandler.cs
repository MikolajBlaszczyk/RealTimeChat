using RealTimeChat.AccountLogic.Models;

namespace RealTimeChat.AccountLogic.Interfaces;

public interface IUserAccountRequestHandler
{
    Task<ResponseModel> HandleRegisterRequest(IUserModel user, CancellationToken token);
    Task<ResponseModel> HandleLoginRequest(IUserModel user, CancellationToken token);
    Task<ResponseModel> HandleLogoutRequest(CancellationToken token);
}