using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.BusinessLogic.AccountLogic.Enums;
using RealTimeChat.BusinessLogic.AccountLogic.Interfaces;
using RealTimeChat.BusinessLogic.AccountLogic.Validators;

namespace RealTimeChat.BusinessLogic.AccountLogic.AccountManager;


public class RegisterManager : IRegisterManager
{

    private SignInManager<IdentityUser> _signInManager;
    private UserManager<IdentityUser> _userManager;

    internal IAccountValidator AccountValidator { get;  }
    internal SignInManager<IdentityUser> SignInManager
    {
        get
        {
            //TODO: Implement custom exception
            if (_signInManager == null)
                throw new Exception("Server Error");

            return _signInManager;
        }
    }
    internal UserManager<IdentityUser> UserManager
    {
        
        get
        {
            //TODO: Implement custom exception
            if (_signInManager == null)
                throw new Exception("Server Error");

            return _userManager;
        }
    }


    public RegisterManager(IAccountValidator accountValidator, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        AccountValidator = accountValidator;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public bool RegisterUser(IUserModel userToRegister, out string message, out ResponseIdentityResult response)
    {
        message = string.Empty;

        var isPasswordValid = AccountValidator.IsPasswordValid(userToRegister.Password, userToRegister.ConfirmPassword, out  message);
        if (isPasswordValid)
        {
            IdentityResult registerResult = CreateUser(userToRegister);

            if (registerResult.Succeeded)
                response = ResponseIdentityResult.Success;
            else
                response = ResponseIdentityResult.UserNotCreated;

            return registerResult.Succeeded;
        }
        else
        {
            response = ResponseIdentityResult.ValidationPasswordFailed;
            return isPasswordValid;
        }
    }


    private IdentityResult CreateUser(IUserModel userToRegister)
    {
        return UserManager.CreateAsync(userToRegister.ConvertToIdentityUser(), userToRegister.Password).Result;
    }

}