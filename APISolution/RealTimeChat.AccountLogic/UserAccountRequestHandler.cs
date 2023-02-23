using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.Models;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.Models;
using Serilog;

namespace RealTimeChat.AccountLogic;

public class UserAccountRequestHandler : IUserAccountRequestHandler
{
    private readonly ILoginManager LoginManager;
    private readonly IRegisterManager RegisterManager;

    public UserAccountRequestHandler(IRegisterManager registerManager, ILoginManager loginManager)
    {
        LoginManager = loginManager;
        RegisterManager = registerManager;
    }

    public async Task<ResponseModel> HandleRegisterRequest(IUserModel user, CancellationToken token)
    {
        try
        {
            var registerResult = await RegisterManager.RegisterUserAsync(user, token);
            
            if (registerResult.Result != ResponseIdentityResult.Success)
            {
                return registerResult;
            }

            var loginResult = await LoginManager.SignInAsync(user, token, null);

            return loginResult;
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
            return await LoginManager.LoginUserAsync(user, token);
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

            return ResponseModel.CreateResponse(ResponseIdentityResult.Success);
        }
        catch (Exception ex)
        {
            return ResponseModel.CreateResponse(ResponseIdentityResult.ServerError, ex.Message);
        }
    }

}
