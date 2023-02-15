
using RealTimeChat.API.Messages;
using RealTimeChat.BusinessLogic.FriendsLogic.Enums;

namespace RealTimeChat.API.Controllers;

public class FriendsCallLogger
{
    internal ILogger<FriendsCallLogger> Logger { get; }

    public FriendsCallLogger(ILogger<FriendsCallLogger> logger)
    {
        Logger = logger;
    }
    
    internal void GenerateResponseLog(FriendsResponseResult result, FriendsRequest request)
    {
        var logLevel = result == FriendsResponseResult.Success ? LogLevel.Information : LogLevel.Error;

        var logMessage = request switch
        {
            FriendsRequest.Add => result == FriendsResponseResult.Success
            ? FriendsControllerMessage.AddSuccess
            : FriendsControllerMessage.AddFail,
            
            FriendsRequest.Get => result == FriendsResponseResult.Success
            ? FriendsControllerMessage.GetSuccess
            : FriendsControllerMessage.GetFail,
            
            FriendsRequest.Remove => result == FriendsResponseResult.Success
                ? FriendsControllerMessage.RemoveSuccess
                : FriendsControllerMessage.RemoveFail,
            
            FriendsRequest.GetInvitations => result == FriendsResponseResult.Success
                ? FriendsControllerMessage.GetInvitationsSuccess
                : FriendsControllerMessage.GetInvitationsFail,
            
            FriendsRequest.RespondToInvitation => result == FriendsResponseResult.Success
                ? FriendsControllerMessage.InvitationRespondSuccess
                : FriendsControllerMessage.InvitationRespondFail,
            
            _ => FriendsControllerMessage.Error
        };

        Logger.Log(logLevel, logMessage);
    }
    
    internal void GenerateRequestLog(FriendsRequest request)
    {
        var logMessage = request switch
        {
            FriendsRequest.Add => FriendsControllerMessage.AddRequest,
            FriendsRequest.Get => FriendsControllerMessage.GetRequest,
            FriendsRequest.Remove => FriendsControllerMessage.RemoveRequest,
            FriendsRequest.GetInvitations => FriendsControllerMessage.GetInvitationsRequest,
            FriendsRequest.RespondToInvitation => FriendsControllerMessage.InvitationRespondRequest,
            _ => FriendsControllerMessage.Error
        };

        Logger.Log(LogLevel.Information, logMessage);
    }
}