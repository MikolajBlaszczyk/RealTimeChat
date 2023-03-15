using Microsoft.AspNetCore.Identity;
using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.Models;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.AccountLogic.AccountManager;

public class LoginManager : ILoginManager
{
    private SignInManager<ApplicationUser> _singInManager;
    public IAccountValidator Validator { get; }
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
    

    public LoginManager(IAccountValidator validator, SignInManager<ApplicationUser> singInManager)
    {
        Validator = validator;
        _singInManager = singInManager;
    }

    public async Task<ResponseModel> LoginUserAsync(IUserModel user, CancellationToken token)
    {
        var isValid = Validator.IsPasswordValid(user.Password);
        
        if (isValid)
        {
            var result = await SignInAsync(user, token);

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
    }


 

}