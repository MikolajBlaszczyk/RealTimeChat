using Microsoft.EntityFrameworkCore;
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
    public IDbUserHelper UserHelper { get; }

    public InvitationsManager(ApplicationContext context, IDbUserHelper userHelper)
    {
        Context = context;
        UserHelper = userHelper;
    }
    public async Task<InvitationStatus> CreateInvitation(string userId, string friendId)
    {
        InvitationModel newInvitation = new InvitationModel();
        newInvitation.Status = "Pending";
        newInvitation.SenderId = userId;
        newInvitation.ResponderId = friendId;

        var dbStatus = await Context.Invitations.AddAsync(newInvitation);
        if (dbStatus.State == EntityState.Added)
        {
            await Context.SaveChangesAsync();
            return InvitationStatus.Pending;
        }

        return InvitationStatus.Error;
    }

    public async Task<InvitationStatus> UpdateInvitation(string senderId, string responderId, bool response)
    {
        var invitation = await UserHelper.FindInvitation(senderId, responderId);
        if (invitation == null)
            return InvitationStatus.Error;

        InvitationStatus status;

        if (response == false)
        {
            invitation.Status = "Declined";
            Context.Invitations.Update(invitation);
            status = InvitationStatus.Declined;
        }
        else
        {
            invitation.Status = "Accepted";
            Context.Invitations.Remove(invitation);
            status = InvitationStatus.Accepted;
        }

        await Context.SaveChangesAsync();
        return status;
    }
}