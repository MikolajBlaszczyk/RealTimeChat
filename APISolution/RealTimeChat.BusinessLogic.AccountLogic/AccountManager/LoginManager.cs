using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.BusinessLogic.AccountLogic.Enums;
using RealTimeChat.BusinessLogic.AccountLogic.Interfaces;
using RealTimeChat.BusinessLogic.AccountLogic.SessionManager;
using RealTimeChat.BusinessLogic.AccountLogic.Validators;

namespace RealTimeChat.BusinessLogic.AccountLogic.AccountManager;

public class LoginManager : ILoginManager
{
    private SignInManager<IdentityUser> _singInManager;
    private readonly SessionHandler _sessionHandler;

    public IAccountValidator Validator { get; }
    public SignInManager<IdentityUser> SignInManager
    {
        get
        {
            //TODO implement custom exception
            if (_singInManager == null)
                throw new Exception("Server error");

            return _singInManager;
        }
    }


    public LoginManager(IAccountValidator validator, SignInManager<IdentityUser> singInManager, SessionHandler sessionHandler)
    {
        Validator = validator;
        _singInManager = singInManager;
        _sessionHandler = sessionHandler;
    }

    public bool LoginUser(IUserModel user, out string message, out ResponseIdentityResult res)
    {
        message = string.Empty;

        var isValid = Validator.IsPasswordValid(user.Password);
        if (isValid)
        {
            SignInResult result = SignIn(user, out message, out res);

            var guid = 

            _sessionHandler.InitializeSession(guid);

            return result.Succeeded;
        }
        else
        {
            //TODO message should be changes inside of IsPasswordValidFunction
            message = "Password is Too short";
            res = ResponseIdentityResult.WrongCredentials;
        }

        return isValid;
    }

    public SignInResult SignIn(IUserModel user, out string message, out ResponseIdentityResult res)
    {
        message = string.Empty;

        SignInResult signInResult =  SignInManager.PasswordSignInAsync(user.Username, user.Password, false, false).Result;

        if (signInResult.Succeeded) 
            res = ResponseIdentityResult.Success;
        else
            res = ResponseIdentityResult.WrongCredentials;

        return signInResult;
    }

  

    public async Task SignOut()
    {
        await SignInManager.SignOutAsync();

        var guid = SignInManager.Context.Session.Id;

        _sessionHandler.TerminateSession(guid);
    }
}