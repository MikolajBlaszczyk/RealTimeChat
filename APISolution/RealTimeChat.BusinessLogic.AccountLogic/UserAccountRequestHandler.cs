using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RealTimeChat.BusinessLogic.AccountLogic.Interfaces;
using RealTimeChat.BusinessLogic.AccountLogic.Enums;
using RealTimeChat.BusinessLogic.AccountLogic.Models;
using Serilog;

namespace RealTimeChat.BusinessLogic.AccountLogic;

public class UserAccountRequestHandler : IUserAccountRequestHandler
{
    private ILogger<IUserAccountRequestHandler> Logger { get; }
    private readonly ILoginManager LoginManager;
    private readonly IRegisterManager RegisterManager;

    public UserAccountRequestHandler(IRegisterManager registerManager, ILoginManager loginManager, ILogger<IUserAccountRequestHandler> logger)
    {
        Logger = logger;
        LoginManager = loginManager;
        RegisterManager = registerManager;
    }

    public async Task<ResponseModel> HandleRegisterRequest(IUserModel user)
    {
        try
        {
            var registerResult = await RegisterManager.RegisterUserAsync(user);
            
            if (registerResult.Result != ResponseIdentityResult.Success)
            {
                return registerResult;
            }

            var loginResult = await LoginManager.SignInAsync(user);

            return loginResult;
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, "Exception message: {Message}", ex.StackTrace);
            return ResponseModel.CreateResponse(ResponseIdentityResult.ServerError, ex.Message);
        }
    }

    public async Task<ResponseModel> HandleLoginRequest(IUserModel user)
    {
        try
        {
            return await LoginManager.LoginUserAsync(user);
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, "Exception message: {Message}", ex.StackTrace);
            return ResponseModel.CreateResponse(ResponseIdentityResult.WrongCredentials, ex.Message);
        }
    }

    public async Task<ResponseModel> HandleLogoutRequest()
    {
        try
        {
            await LoginManager.SignOutAsync();

            return ResponseModel.CreateResponse(ResponseIdentityResult.Success);
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, "Exception message: {Message}", ex.StackTrace);
            return ResponseModel.CreateResponse(ResponseIdentityResult.ServerError, ex.Message);
        }
    }

}
