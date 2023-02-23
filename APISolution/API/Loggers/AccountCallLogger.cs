using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.API.Messages;
using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.API.enums;

namespace RealTimeChat.API.Controllers;

public class AccountCallLogger
{
    internal ILogger<AccountCallLogger> Logger { get; }

    public AccountCallLogger(ILogger<AccountCallLogger> logger)
    {
        Logger = logger;
    }    

    internal void GenerateResponseLog(ResponseIdentityResult result, AccountRequest request)
    {
        var logLevel = result == ResponseIdentityResult.Success ? LogLevel.Information : LogLevel.Error;

        var logMessage = request switch
        {
            AccountRequest.Login => result == ResponseIdentityResult.Success
                ? AccountControllerMessage.LoginSuccess
                : AccountControllerMessage.LoginFailed,
            
            AccountRequest.Register => result == ResponseIdentityResult.Success
                ? AccountControllerMessage.RegisterSuccess
                : AccountControllerMessage.RegisterFailed,
            
            AccountRequest.Logout => result == ResponseIdentityResult.Success
                ? AccountControllerMessage.LogoutSuccess
                : AccountControllerMessage.LogoutFailed,
            
            _ => AccountControllerMessage.Error
        };

        Logger.Log(logLevel, logMessage);
    }

    internal void GenerateRequestLog(AccountRequest request)
    {
        var logMessage = request switch
        {
            AccountRequest.Login => AccountControllerMessage.LoginRequest,
            
            AccountRequest.Register => AccountControllerMessage.RegisterRequest,
            
            AccountRequest.Logout =>AccountControllerMessage.LogoutRequest,
                
            _ => AccountControllerMessage.Error
        };

        Logger.Log(LogLevel.Information, logMessage);
    }
}