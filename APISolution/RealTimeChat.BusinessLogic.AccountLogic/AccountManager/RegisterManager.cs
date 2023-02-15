using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.API.DataAccess.Models;
using RealTimeChat.BusinessLogic.AccountLogic.Enums;
using RealTimeChat.BusinessLogic.AccountLogic.Interfaces;
using RealTimeChat.BusinessLogic.AccountLogic.Messages;
using RealTimeChat.BusinessLogic.AccountLogic.Models;
using RealTimeChat.BusinessLogic.AccountLogic.Validators;

namespace RealTimeChat.BusinessLogic.AccountLogic.AccountManager;


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
                throw new Exception(ResponseResultMessage.ServerError);

            return _signInManager;
        }
    }
    private UserManager<ApplicationUser> UserManager
    {
        
        get
        {
            //TODO: Implement custom exception
            if (_signInManager == null)
                throw new Exception(ResponseResultMessage.ServerError);

            return _userManager;
        }
    }


    public RegisterManager(IAccountValidator accountValidator, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        AccountValidator = accountValidator;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<ResponseModel> RegisterUserAsync(IUserModel userToRegister)
    {
        string message = string.Empty;
        
        var isPasswordValid = AccountValidator.IsPasswordValid(userToRegister.Password, userToRegister.ConfirmPassword, ref message);
        if (isPasswordValid)
        {
            IdentityResult registerResult = await  CreateUserAsync(userToRegister);
            
            if (registerResult.Succeeded)
                return ResponseModel.CreateResponse(ResponseIdentityResult.Success);
            else
                return ResponseModel.CreateResponse(ResponseIdentityResult.UserNotCreated, ResponseResultMessage.UserNotCreated);
        }
        else
        {
            return ResponseModel.CreateResponse(ResponseIdentityResult.ValidationPasswordFailed, message);
        }
    }
    
    private async Task<IdentityResult> CreateUserAsync(IUserModel userToRegister)
    {
        IdentityResult result;
        var user = userToRegister.ConvertToIdentityUser();

        result = await UserManager.CreateAsync(user, userToRegister.Password);
        // is it necessary? 
        //result = await UserManager.AddClaimAsync(user, new Claim("FUID", await UserManager.GetUserIdAsync(user)));

        return result;

    }

}