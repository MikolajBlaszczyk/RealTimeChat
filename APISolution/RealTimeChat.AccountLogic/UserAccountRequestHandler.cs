using RealTimeChat.AccountLogic.AccountManager;
using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.Models;

namespace RealTimeChat.AccountLogic;

public class UserAccountRequestHandler : IUserAccountRequestHandler
{
    private readonly ClaimsManager ClaimsManager;
    private readonly ILoginManager LoginManager;
    private readonly IRegisterManager RegisterManager;
    private readonly  ISessionHandler SessionHandler;

    public UserAccountRequestHandler(IRegisterManager registerManager, ILoginManager loginManager, ClaimsManager claimsManager, ISessionHandler sessionHandler)
    {
        ClaimsManager = claimsManager;
        LoginManager = loginManager;
        RegisterManager = registerManager;
        SessionHandler = sessionHandler;
    }

    public async Task<ResponseModel> HandleRegisterRequest(IUserModel user, CancellationToken token)
    {
        try
        {
            var registerResult = await RegisterManager.RegisterUserAsync(user, token);
            if (registerResult.Result != ResponseIdentityResult.Success)
                return registerResult;


            var loginResult = await LoginManager.SignInAsync(user, token);
            if (loginResult.Result != ResponseIdentityResult.Success)
                return loginResult;


            var claimsResult = await ClaimsManager.RequireClaims(user);
            if(claimsResult.Result != ResponseIdentityResult.Success)
                return claimsResult;
            

            return await SessionHandler.InitializeSession();
        }
        catch (Exception ex)
        {
            return ResponseModel.CreateResponse(ResponseIdentityResult.ServerError, ex.Message);
        }
    }

    public async Task<ResponseModel> HandleLoginRequest(IUserModel user, CancellationToken token)
    {
        try
        {
            var loginResult = await LoginManager.LoginUserAsync(user, token);
            if(loginResult.Result != ResponseIdentityResult.Success)
                return loginResult;


            var claimsResult = await ClaimsManager.SetContextClaim(user);
            if(claimsResult.Result != ResponseIdentityResult.Success)
                return claimsResult;


            return await SessionHandler.InitializeSession();
        }
        catch (Exception ex)
        {
            return ResponseModel.CreateResponse(ResponseIdentityResult.ServerError, ex.Message);
        }
    }

    public async Task<ResponseModel> HandleLogoutRequest(CancellationToken token)
    {
        try
        {
            await LoginManager.SignOutAsync(token);
            await SessionHandler.TerminateSession();

            return ResponseModel.CreateResponse(ResponseIdentityResult.Success);
        }
        catch (Exception ex)
        {
            return ResponseModel.CreateResponse(ResponseIdentityResult.ServerError, ex.Message);
        }
    }

}
