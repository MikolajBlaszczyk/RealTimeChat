using RealTimeChat.BusinessLogic.AccountLogic.Enums;

namespace RealTimeChat.BusinessLogic.AccountLogic.Interfaces;

public interface IUserAccountRequestHandler
{
    bool HandleRegisterRequest(IUserModel user, out string message, out ResponseIdentityResult response);
    bool HandleLoginRequest(IUserModel user, out string message, out ResponseIdentityResult response);
    bool HandleLogoutRequest(out string message, out ResponseIdentityResult response);
}