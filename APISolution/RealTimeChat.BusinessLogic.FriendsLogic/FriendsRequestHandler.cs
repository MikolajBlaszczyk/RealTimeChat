using Microsoft.EntityFrameworkCore;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.DataAccess.Models;
using RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;
using RealTimeChat.BusinessLogic.FriendsLogic.Models;
using RealTimeChat.BusinessLogic.FriendsLogic;
using RealTimeChat.BusinessLogic.FriendsLogic.Enums;
using RealTimeChat.BusinessLogic.FriendsLogic.FriendsManagerDir;
using RealTimeChat.BusinessLogic.FriendsLogic.Helpers;

namespace RealTimeChat.BusinessLogic.FriendsLogic;

public class FriendsRequestHandler : IFriendsRequestHandler
{
    public ApplicationContext Context { get; }
    public IFriendsManager FriendsManager { get; }
    public IInvitationsManager InvitationsManager { get; }
    public IDbUserHelper DbUserHelper { get; }


    public FriendsRequestHandler(ApplicationContext context, IFriendsManager friendsManager,IInvitationsManager invitationsManager , IDbUserHelper dbUserHelper)
    {
        Context = context;
        FriendsManager = friendsManager;
        InvitationsManager = invitationsManager;
        DbUserHelper = dbUserHelper;
    }
    public async Task<ResponseModel> AddFriend(string userId, string friendUsername)
    {
        var friendId = await DbUserHelper.FindUserUsername(friendUsername);
        
        
        if (friendId == null)
            return ResponseModel.CreateResponse(FriendsResponseResult.Fail, "User does not exist");
        
        if(friendId == userId)
            return ResponseModel.CreateResponse(FriendsResponseResult.Fail, "You cannot add yourself");
        
        var result = await FriendsManager.AddFriend(userId, friendId);

        return result;
    }

    public async Task<ResponseModel> GetAllFriends(string userId)
    {

        var response = await FriendsManager.GetAllFriends(userId);
        
        return response;
    }

    public async Task<ResponseModel> GetAllInvitations(string userId)
    {
        var response = await InvitationsManager.GetAllInvitations(userId);

        return response;
    }

    public async Task<ResponseModel> InvitationResponse(string userId, string friendUsername, bool response)
    {
        var friendId = await DbUserHelper.FindUserUsername(friendUsername);
        
        if (friendId == null)
            return ResponseModel.CreateResponse(FriendsResponseResult.InvalidUser, "Invalid user");

        var result = await InvitationsManager.UpdateInvitation(friendId, userId, response);

        if (result.Message == "Invitation accepted")
        {
            var managerResponse = await FriendsManager.CreateFriendship(userId, friendId);
            if (managerResponse.Result == FriendsResponseResult.Fail)
                return managerResponse;
        }

        return result;
    }

    public async Task<ResponseModel> RemoveFriend(string userId, string friendUsername)
    {
        var friendId = await DbUserHelper.FindUserUsername(friendUsername);
        
        
        if (friendId == null)
            return ResponseModel.CreateResponse(FriendsResponseResult.Fail, "User does not exist");
        
        if(friendId == userId)
            return ResponseModel.CreateResponse(FriendsResponseResult.Fail, "You cannot remove yourself");

        var result = await FriendsManager.RemoveFriend(userId, friendId);

        return result;
    }
}