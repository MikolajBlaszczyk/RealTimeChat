using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.Models;
using RealTimeChat.DataAccess.DataAccess;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.AccountLogic.AccountManager;

public class LoginManager : ILoginManager
{
    private SignInManager<ApplicationUser> _singInManager;
    private readonly ISessionHandler _sessionHandler;

    public IAccountValidator Validator { get; }
    
    public IMemoryCache Cache { get; }

    public SignInManager<ApplicationUser> SignInManager
    {
        get
        {
            //TODO implement custom exception
            if (_singInManager == null)
                throw new Exception("Server error");

            return _singInManager;
        }
    }
    
    public LoginManager(IAccountValidator validator, SignInManager<ApplicationUser> singInManager, ISessionHandler sessionHandler)
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
        
        
        SignInResult signInResult =  await SignInManager.PasswordSignInAsync(user.Username, user.Password, true, false);

        var cookie = SignInManager.Context.Response.Cookies;
        if (signInResult.Succeeded)
        {

            return ResponseModel.CreateResponse(ResponseIdentityResult.Success);
        }
        else
        {
            return ResponseModel.CreateResponse(ResponseIdentityResult.WrongCredentials);
        }
    }

    public async Task SignOutAsync(CancellationToken token)
    {
        await SignInManager.SignOutAsync();

        _sessionHandler.TerminateSession();
    }


 

}