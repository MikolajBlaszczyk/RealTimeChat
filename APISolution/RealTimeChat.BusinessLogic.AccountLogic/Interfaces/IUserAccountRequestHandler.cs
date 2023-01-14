using RealTimeChat.BusinessLogic.AccountLogic.Models;

namespace RealTimeChat.BusinessLogic.AccountLogic.Interfaces;

public interface IUserAccountRequestHandler
{
    Task<ResponseModel> HandleRegisterRequest(IUserModel user);
    Task<ResponseModel> HandleLoginRequest(IUserModel user);
    Task<ResponseModel> HandleLogoutRequest();
}