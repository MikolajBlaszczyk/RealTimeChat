using Microsoft.Extensions.Logging;
using RealTimeChat.FriendsLogic.Enums;
using RealTimeChat.FriendsLogic.Interfaces;
using RealTimeChat.FriendsLogic.Models;

namespace RealTimeChat.FriendsLogic;

public class FriendsRequestHandler : IFriendsRequestHandler
{
    private IFriendsManager FriendsManager { get; }
    private IInvitationsManager InvitationsManager { get; }
    private ILogger<IFriendsRequestHandler> Logger { get; }


    public FriendsRequestHandler(IFriendsManager friendsManager,IInvitationsManager invitationsManager, ILogger<IFriendsRequestHandler> logger)
    {
        FriendsManager = friendsManager;
        InvitationsManager = invitationsManager;
        Logger = logger;
    }
    public async Task<ResponseModel> AddFriend(string userId, string friendUsername)
    {

        try
        {
            var result = await FriendsManager.AddFriend(userId, friendUsername);

            return result;
        }
        catch (ArgumentException ex)
        {
            Logger.Log(LogLevel.Warning, "Argument exception message: {Message}. Stack trace: {StackTrace}", ex.Message, ex.StackTrace);
            return ResponseModel.CreateResponse(FriendsResponseResult.InvalidUser, ex.Message);
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, "Exception message: {Message}", ex.StackTrace);
            return ResponseModel.CreateResponse(FriendsResponseResult.ServerError, ex.Message);
        }

    }

    public async Task<ResponseModel> GetAllFriends(string userId)
    {
        
        try
        {
            var response = await FriendsManager.GetAllFriends(userId);
            return response;
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, "Exception message: {Message}", ex.StackTrace);
            return ResponseModel.CreateResponse(FriendsResponseResult.ServerError, ex.Message);
        }
        
    }

    public async Task<ResponseModel> GetAllInvitations(string userId)
    {

        try
        {
            var response = await InvitationsManager.GetAllInvitations(userId);
            return response;
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, "Exception message: {Message}", ex.StackTrace);
            return ResponseModel.CreateResponse(FriendsResponseResult.ServerError, ex.Message);
        }
        
    }

    public async Task<ResponseModel> InvitationResponse(string userId, string friendUsername, bool response)
    {
        try
        {

            var invitationResult = await InvitationsManager.UpdateInvitation(friendUsername, userId, response);

            if (invitationResult == Enums.InvitationStatus.Declined)
                return ResponseModel.CreateResponse(FriendsResponseResult.Success, "Invitation declined");
            
            var friendshipResponse = await FriendsManager.CreateFriendship(userId, friendUsername);

            return friendshipResponse;
        }
        catch (ArgumentException ex)
        {
            Logger.Log(LogLevel.Warning, "Argument exception message: {Message}. Stack trace: {StackTrace}", ex.Message, ex.StackTrace);
            return ResponseModel.CreateResponse(FriendsResponseResult.InvalidUser, ex.Message);
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, "Exception message: {Message}", ex.StackTrace);
            return ResponseModel.CreateResponse(FriendsResponseResult.ServerError, ex.Message);
        }
        
    }

    public async Task<ResponseModel> RemoveFriend(string userId, string friendUsername)
    {
        try
        {
            
            var result = await FriendsManager.RemoveFriend(userId, friendUsername);

            return result;
        }
        catch (ArgumentException ex)
        {
            Logger.Log(LogLevel.Warning, "Argument exception message: {Message}. Stack trace: {StackTrace}", ex.Message, ex.StackTrace);
            return ResponseModel.CreateResponse(FriendsResponseResult.InvalidUser, ex.Message);
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Error, "Exception message: {Message}", ex.StackTrace);
            return ResponseModel.CreateResponse(FriendsResponseResult.ServerError, ex.Message);
        }
       
    }
}