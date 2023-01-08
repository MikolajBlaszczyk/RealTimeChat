using Microsoft.AspNetCore.Identity;    
using RealTimeChat.BusinessLogic.AccountLogic.Interfaces;
using RealTimeChat.BusinessLogic.AccountLogic.Enums;
using RealTimeChat.BusinessLogic.AccountLogic.SessionManager;

namespace RealTimeChat.BusinessLogic.AccountLogic;

public class UserAccountRequestHandler : IUserAccountRequestHandler
{
    private const bool ServerError = false;

    private readonly ILoginManager LoginManager;
    private readonly IRegisterManager RegisterManager;

    public UserAccountRequestHandler(IRegisterManager registerManager, ILoginManager loginManager)
    {
        LoginManager = loginManager;
        RegisterManager = registerManager;
    }

    public bool HandleRegisterRequest(IUserModel user, out string message, out ResponseIdentityResult res)
    {
        try
        {
            bool isRegisterSuccess =RegisterManager.RegisterUser(user,out message,out res);
            if (isRegisterSuccess == false)
            {
                return isRegisterSuccess;
            }

            SignInResult loginResult = LoginManager.SignIn(user, out message, out res);

            return loginResult.Succeeded;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            res = ResponseIdentityResult.ServerError;
            return ServerError;
        }
    }

    public bool HandleLoginRequest(IUserModel user, out string message, out ResponseIdentityResult res)
    {
        //might need to add claims (perhaps when registering user, i don't remember :)) 
        try
        {
            bool isLoginSuccess =  LoginManager.LoginUser(user, out message, out res);

            return isLoginSuccess;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            res = ResponseIdentityResult.ServerError;
            return ServerError;
        }
    }

    public  bool HandleLogoutRequest(out string message, out ResponseIdentityResult res)
    {
        try
        {
            Task task = LoginManager.SignOut();
            Task.WaitAll(task);

            res = ResponseIdentityResult.Success;
            message = string.Empty;
            return true;
        }
        catch (Exception ex)
        {
            res = ResponseIdentityResult.ServerError;
            message = ex.Message;
            return false;
        }
    }

}
