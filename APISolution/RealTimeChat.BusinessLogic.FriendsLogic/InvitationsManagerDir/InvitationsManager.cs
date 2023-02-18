using Microsoft.EntityFrameworkCore;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.DataAccess.Models;
using RealTimeChat.BusinessLogic.FriendsLogic.Enums;
using RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;
using RealTimeChat.BusinessLogic.FriendsLogic.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.InvitationsManagerDir;

public class InvitationsManager : IInvitationsManager
{
    public ApplicationContext Context { get; set; }

    public InvitationsManager(ApplicationContext context)
    {
        Context = context;
    }
    public async Task<InvitationStatus> CreateInvitation(string UserId, string FriendId)
    {
        
        InvitationModel newInvitation = new InvitationModel();
        newInvitation.Status = "Pending";
        newInvitation.SenderId = UserId;
        newInvitation.ResponderId = FriendId;
        
        
        var dbStatus = await Context.Invitations.AddAsync(newInvitation);

        if(dbStatus.State == EntityState.Added)
            return  InvitationStatus.Pending;

        return InvitationStatus.Error;
    }

    public Task<InvitationStatus> UpdateInvitation(string UserId, string FriendId, bool Response)
    {
        throw new NotImplementedException();
    }
}