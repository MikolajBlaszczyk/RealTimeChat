using Microsoft.AspNetCore.Identity;
using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.Models;

namespace RealTimeChat.AccountLogic.AccountManager;

public class LoginManager : ILoginManager
{
    private SignInManager<IdentityUser> _singInManager;
    private readonly ISessionHandler _sessionHandler;

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
    
    public LoginManager(IAccountValidator validator, SignInManager<IdentityUser> singInManager, ISessionHandler sessionHandler)
    {
        Validator = validator;
        _singInManager = singInManager;
        _sessionHandler = sessionHandler;
    }

    public async Task<ResponseModel> LoginUserAsync(IUserModel user, CancellationToken token)
    {
        var isValid = Validator.IsPasswordValid(user.Password);
        if (isValid)
        {
            var result = await SignInAsync(user, token);
            
            await _sessionHandler.InitializeSession();

            return result;
        }
        else
        { 
            return ResponseModel.CreateResponse(ResponseIdentityResult.WrongCredentials, "Password is Too short");
        }

    }

    public async Task<ResponseModel> SignInAsync(IUserModel user, CancellationToken token)
    {
        SignInResult signInResult =  await SignInManager.PasswordSignInAsync(user.Username, user.Password, false, false);

        if (signInResult.Succeeded)
            return ResponseModel.CreateResponse(ResponseIdentityResult.Success);
        else
            return ResponseModel.CreateResponse(ResponseIdentityResult.WrongCredentials);

    }

  

    public async Task SignOutAsync(CancellationToken token)
    {
        await SignInManager.SignOutAsync();

        _sessionHandler.TerminateSession();
    }
}