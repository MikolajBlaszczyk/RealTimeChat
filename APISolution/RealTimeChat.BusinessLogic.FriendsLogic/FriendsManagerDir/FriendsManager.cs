﻿using Microsoft.EntityFrameworkCore;
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

    public FriendsManager(ApplicationContext context, IInvitationsManager invitationsManager)
    {
        Context = context;
        InvitationsManager = invitationsManager;
    }
    
    public async Task<ResponseModel> AddFriend(string UserId, string FriendId)
    {
        var friend = await Context.Friends.FirstOrDefaultAsync(f => f.FriendId == FriendId);
        if (friend != null)
            return ResponseModel.CreateResponse(FriendsResponseResult.AlreadyFriend, "Already befriended.");
    
        var invitation = await Context.Invitations.FirstOrDefaultAsync(i => i.ResponderId == FriendId);
        if (invitation != null)
            return ResponseModel.CreateResponse(FriendsResponseResult.AlreadyFriend, "Invitation pending.");

        // create invitation
        var status = await InvitationsManager.CreateInvitation(UserId, FriendId);

        if (status == InvitationStatus.Error)
            return ResponseModel.CreateResponse(FriendsResponseResult.ServerError, "Server error.");
        
        return ResponseModel.CreateResponse(FriendsResponseResult.Success, "Invitation send.");
    }
}