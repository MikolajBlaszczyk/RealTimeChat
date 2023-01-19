using RealTimeChat.AccountLogic.Models;

namespace RealTimeChat.AccountLogic.Interfaces;

public interface IUserAccountRequestHandler
{
    Task<ResponseModel> HandleRegisterRequest(IUserModel user);
    Task<ResponseModel> HandleLoginRequest(IUserModel user);
    Task<ResponseModel> HandleLogoutRequest();
}