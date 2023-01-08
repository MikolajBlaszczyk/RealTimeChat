using Microsoft.AspNetCore.Identity;
using RealTimeChat.BusinessLogic.AccountLogic.Enums;

namespace RealTimeChat.BusinessLogic.AccountLogic.Interfaces;

public interface ILoginManager
{
    bool LoginUser(IUserModel user, out string message, out ResponseIdentityResult res);
    SignInResult SignIn(IUserModel user, out string message, out ResponseIdentityResult res);
    Task SignOut();
}