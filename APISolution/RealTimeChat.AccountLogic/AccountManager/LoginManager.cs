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
    public AccountDataAccess DataAccess { get; }
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
    
    public LoginManager(IAccountValidator validator, SignInManager<ApplicationUser> singInManager, ISessionHandler sessionHandler,
        AccountDataAccess dataAccess)
    {
        Validator = validator;
        DataAccess = dataAccess;
        _singInManager = singInManager;
        _sessionHandler = sessionHandler;
    }

    public async Task<ResponseModel> LoginUserAsync(IUserModel user, CancellationToken token)
    {
        var isValid = Validator.IsPasswordValid(user.Password);
        if (isValid)
        {

            var principal = await ClaimGuid(DataAccess.GetUserGuid(user.Username));

            var result = await SignInAsync(user, token, principal);


            await _sessionHandler.InitializeSession();

            return result;
        }
        else
        { 
            return ResponseModel.CreateResponse(ResponseIdentityResult.WrongCredentials, "Password is Too short");
        }

    }

    public async Task<ResponseModel> SignInAsync(IUserModel user, CancellationToken token, Claim claim)
    {
        
        
        SignInResult signInResult =  await SignInManager.PasswordSignInAsync(user.Username, user.Password, true, false);

        var cookie = SignInManager.Context.Response.Cookies;
        if (signInResult.Succeeded)
        {
            await CreateGuidClaim(user, claim);

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


    private async Task CreateGuidClaim(IUserModel user, Claim claim)
    {
        var userToFind = await SignInManager.UserManager.FindByNameAsync(user.Username);
        await SignInManager.UserManager.AddClaimAsync(userToFind, claim);
        await SignInManager.RefreshSignInAsync(userToFind);

        ClaimsIdentity claims = new ClaimsIdentity(new[] { claim });
        SignInManager.Context.User.AddIdentity(claims);
    }

    public async Task<Claim> ClaimGuid(string? Guid)
    {
        //TODO: add message
        if (Guid is null)
            throw new ArgumentNullException();


        return new Claim("GUID", Guid);
    }

}