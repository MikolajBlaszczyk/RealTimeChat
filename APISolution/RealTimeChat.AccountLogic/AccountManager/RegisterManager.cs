using Microsoft.AspNetCore.Identity;
using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.Models;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.AccountLogic.AccountManager;


public class RegisterManager : IRegisterManager
{

    private IAccountValidator AccountValidator { get;  }
    public SignInManager<ApplicationUser> SignInManager { get; }
    public UserManager<ApplicationUser> UserManager { get; }


    public RegisterManager(IAccountValidator accountValidator, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        AccountValidator = accountValidator;
        SignInManager = signInManager;
        UserManager = userManager;
    }

    public async Task<ResponseModel> RegisterUserAsync(IUserModel userToRegister, CancellationToken token)
    {
        string message = string.Empty;
        
        var isPasswordValid = AccountValidator.IsPasswordValid(userToRegister.Password, userToRegister.ConfirmPassword, ref message);

        if (isPasswordValid)
        {
            IdentityResult registerResult = await CreateUserAsync(userToRegister, token);

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
        var user = userToRegister.ConvertToApplicationUser();

        IdentityResult result = await UserManager.CreateAsync(user, userToRegister.Password);
        
        return result;

    }


   


}