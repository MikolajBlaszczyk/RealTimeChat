using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.Models;
using RealTimeChat.AccountLogic.SessionManager;
using RealTimeChat.API.DataAccess.Models;
using RealTimeChat.DataAccess.DataAccess;

namespace RealTimeChat.AccountLogic.AccountManager;

public class LoginManager : ILoginManager
{
    private SignInManager<ApplicationUser> _singInManager;
    private readonly ISessionHandler _sessionHandler;
    private readonly IHttpContextAccessor _accessor;

    public IAccountValidator Validator { get; }
    public AccountDataAccess DataAccess { get; }

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
    
    public LoginManager(IAccountValidator validator, SignInManager<ApplicationUser> singInManager, ISessionHandler sessionHandler, IHttpContextAccessor accessor,AccountDataAccess dataAccess)
    {
        Validator = validator;
        DataAccess = dataAccess;
        _singInManager = singInManager;
        _sessionHandler = sessionHandler;
        _accessor = accessor;
    }

    public async Task<ResponseModel> LoginUserAsync(IUserModel user, CancellationToken token)
    {
        var isValid = Validator.IsPasswordValid(user.Password);
        if (isValid)
        {
            var result = await SignInAsync(user, token);

            await ClaimGuid(DataAccess.GetUserGuid(user.Username));

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

    public async Task ClaimGuid(string? Guid)
    {
        //TODO: add message
        if(Guid is null)
            throw new ArgumentNullException();

        var claims = new List<Claim> { new Claim("GUID", Guid) };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        _accessor.HttpContext.User = principal;
    }

    public async Task SignOutAsync(CancellationToken token)
    {
        await SignInManager.SignOutAsync();

        _sessionHandler.TerminateSession();
    }
}