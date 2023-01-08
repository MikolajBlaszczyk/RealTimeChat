using RealTimeChat.BusinessLogic.AccountLogic.Enums;

namespace RealTimeChat.BusinessLogic.AccountLogic.Interfaces;

public interface IRegisterManager
{
    bool RegisterUser(IUserModel userToRegister, out string message, out ResponseIdentityResult response);
}