using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.DataAccess.Models;
using RealTimeChat.BusinessLogic.FriendsLogic.Enums;
using RealTimeChat.BusinessLogic.FriendsLogic.Helpers;
using RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;
using RealTimeChat.BusinessLogic.FriendsLogic.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.InvitationsManagerDir;

public class InvitationsManager : IInvitationsManager
{
    public ApplicationContext Context { get; set; }
    public IDbUserHelper DbUserHelper { get; }


    public InvitationsManager(ApplicationContext context, IDbUserHelper dbUserHelper)
    {
        Context = context;
        DbUserHelper = dbUserHelper;
    }
    public async Task<ResponseModel> CreateInvitation(string userId, string friendId)
    {
        InvitationModel newInvitation = new InvitationModel();
        newInvitation.Status = "Pending";
        newInvitation.SenderId = userId;
        newInvitation.ResponderId = friendId;

        var dbStatus = await Context.Invitations.AddAsync(newInvitation);
        if (dbStatus.State == EntityState.Added)
        {
            await Context.SaveChangesAsync();
            return ResponseModel.CreateResponse(FriendsResponseResult.Success, "Invitation send");
        }

        return ResponseModel.CreateResponse(FriendsResponseResult.ServerError, "Internal error");
    }

    public async Task<ResponseModel> UpdateInvitation(string senderId, string userId, bool response)
    {
        var invitation = await DbUserHelper.FindInvitation(senderId, userId);

        if (invitation == null)
            return ResponseModel.CreateResponse(FriendsResponseResult.Fail, "No invitation from this user");

        string message;

        if (response == false)
        {
            invitation.Status = "Declined";
            Context.Invitations.Update(invitation);
            message = "Invitation declined";
        }
        else
        {
            invitation.Status = "Accepted";
            Context.Invitations.Remove(invitation);
            message = "Invitation accepted";
        }

        await Context.SaveChangesAsync();
        return ResponseModel.CreateResponse(FriendsResponseResult.Success, message);
    }

    public async Task<ResponseModel> GetAllInvitations(string userId)
    {
        var invitations = DbUserHelper.GetAllInvitationsSenders(userId);
        if (invitations == null || invitations.Count == 0)
            return ResponseModel.CreateResponse(FriendsResponseResult.Fail, "No invitations");

        string result = string.Empty;
        foreach (var sender in invitations)
        {
            result += sender.UserName + " ";
        }
        
        return ResponseModel.CreateResponse(FriendsResponseResult.Success, result);
    }
}