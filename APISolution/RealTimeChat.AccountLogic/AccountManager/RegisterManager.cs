using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.Models;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.AccountLogic.AccountManager;


public class RegisterManager : IRegisterManager
{

    private SignInManager<ApplicationUser> _signInManager;
    private UserManager<ApplicationUser> _userManager;
    private IAccountValidator AccountValidator { get;  }
    private SignInManager<ApplicationUser> SignInManager
    {
        get
        {
            //TODO: Implement custom exception
            if (_signInManager == null)
                throw new Exception("Server Error");

            return _signInManager;
        }
    }
    private UserManager<ApplicationUser> UserManager
    {
        get
        {
            //TODO: Implement custom exception
            if (_signInManager == null)
                throw new Exception("Server Error");

            return _userManager;
        }
    }


    public RegisterManager(IAccountValidator accountValidator, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        AccountValidator = accountValidator;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<ResponseModel> RegisterUserAsync(IUserModel userToRegister, CancellationToken token)
    {
        string message = string.Empty;
        
        var isPasswordValid = AccountValidator.IsPasswordValid(userToRegister.Password, userToRegister.ConfirmPassword, ref message);
        if (isPasswordValid)
        {
            IdentityResult registerResult = await  CreateUserAsync(userToRegister, token);

            if (registerResult.Succeeded)
                return ResponseModel.CreateResponse(ResponseIdentityResult.Success);
            else
                return ResponseModel.CreateResponse(ResponseIdentityResult.UserNotCreated);
        }
        else
        {
            return ResponseModel.CreateResponse(ResponseIdentityResult.ValidationPasswordFailed, message);
        }
    }
    
    private async Task<IdentityResult> CreateUserAsync(IUserModel userToRegister, CancellationToken token)
    {
        IdentityResult result;
        var user = userToRegister.ConvertToApplicationUser();

        result = await UserManager.CreateAsync(user, userToRegister.Password);

        if(result.Succeeded)
            result = await UserManager.AddClaimAsync(user, new Claim("GUID", await UserManager.GetUserIdAsync(user)));

        return result;

    }

}