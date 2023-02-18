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
        
        var result = await FriendsManager.AddFriend(userId, friendId);

        return result;
    }

    public async Task<ResponseModel> GetAllFriends(string userId)
    {
        var user = await Context.Users
            .Include(u => u.Friends)
            .FirstOrDefaultAsync(u => u.Id == userId);

        var friends = user.Friends.Select(f => f.Friend).ToList();
        
        var response = ResponseModel.CreateResponse(FriendsResponseResult.Success, friends.ToString()!);
        return response;
    }

    public async Task<ResponseModel> InvitationResponse(string userId, string friendUsername, bool response)
    {
        var friendId = await DbUserHelper.FindUserUsername(friendUsername);

        var result = await InvitationsManager.UpdateInvitation(userId, friendId, response);

        string message = result switch
        {
            InvitationStatus.Accepted => "Invitation accepted",
            InvitationStatus.Declined => "Invitation declined",
            _ => "Internal Error"
        };

        return ResponseModel.CreateResponse(FriendsResponseResult.Success, message);
    }
    
}