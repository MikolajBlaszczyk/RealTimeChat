using Microsoft.EntityFrameworkCore;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.BusinessLogic.FriendsLogic.Enums;
using RealTimeChat.BusinessLogic.FriendsLogic.Helpers;
using RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;
using RealTimeChat.BusinessLogic.FriendsLogic.InvitationsManagerDir;
using RealTimeChat.BusinessLogic.FriendsLogic.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.FriendsManagerDir;

public class FriendsManager : IFriendsManager
{
    public ApplicationContext Context { get; }
    public IInvitationsManager InvitationsManager { get; set; }
    public IDbUserHelper DbUserHelper { get; }

    public FriendsManager(ApplicationContext context, IInvitationsManager invitationsManager, IDbUserHelper dbUserHelper)
    {
        Context = context;
        InvitationsManager = invitationsManager;
        DbUserHelper = dbUserHelper;
    }
    
    public async Task<ResponseModel> AddFriend(string userId, string friendId)
    {
        var friendship = await DbUserHelper.FindFriendship(userId, friendId);
        
        if (friendship != null)
            return ResponseModel.CreateResponse(FriendsResponseResult.AlreadyFriend, "Already befriended.");

        var invitation = await DbUserHelper.FindInvitation(userId, friendId);
        if (invitation != null)
            return ResponseModel.CreateResponse(FriendsResponseResult.AlreadyFriend, "Invitation pending.");

        // create invitation
        var status = await InvitationsManager.CreateInvitation(userId, friendId);

        if (status == InvitationStatus.Error)
            return ResponseModel.CreateResponse(FriendsResponseResult.ServerError, "Server error.");
        
        return ResponseModel.CreateResponse(FriendsResponseResult.Success, "Invitation send.");
    }

}