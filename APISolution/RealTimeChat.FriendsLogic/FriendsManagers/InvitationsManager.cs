using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using RealTimeChat.DataAccess.DataAccess;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Models;
using RealTimeChat.FriendsLogic.Enums;
using RealTimeChat.FriendsLogic.Interfaces;
using RealTimeChat.FriendsLogic.Models;
using InvitationStatus = RealTimeChat.BusinessLogic.FriendsLogic.Enums.InvitationStatus;

namespace RealTimeChat.FriendsLogic.FriendsManagers;

public class InvitationsManager : IInvitationsManager
{
    private ApplicationContext Context { get; set; }
    private FriendsDataAccess FriendsDataAccess { get; }


    public InvitationsManager(ApplicationContext context, FriendsDataAccess friendsDataAccess)
    {
        Context = context;
        FriendsDataAccess = friendsDataAccess;
    }
    
    public async Task<ResponseModel> CreateInvitation(string userId, string friendId)
    {
        InvitationModel newInvitation = new InvitationModel()
        {
            Status = "Pending",
            SenderId = userId,
            ResponderId = friendId
        };
        
        var dbStatus = await Context.Invitations.AddAsync(newInvitation);
        if (dbStatus.State != EntityState.Added)
            throw new Exception("Database fail");
        
        await Context.SaveChangesAsync();
        return ResponseModel.CreateResponse(FriendsResponseResult.Success, "Invitation send");

    }

    public async Task<Enums.InvitationStatus> UpdateInvitation(string friendUsername, string userId, bool response)
    {
        var senderId = await FriendsDataAccess.GetFriendGuidByUserName(friendUsername, userId);
        
        var invitation = await FriendsDataAccess.FindInvitation(senderId, userId);

        if (invitation == null || invitation.Status == "Declined")
            throw new ArgumentException("No invitation from this user");

        Enums.InvitationStatus invitationStatus;
        EntityEntry<InvitationModel> dbResponse;

        if (response == false)
        {
            invitation.Status = "Declined";
            dbResponse =  Context.Invitations.Update(invitation);
            invitationStatus = Enums.InvitationStatus.Declined;
        }
        else
        {
            invitation.Status = "Accepted";
            dbResponse = Context.Invitations.Remove(invitation);
            invitationStatus = Enums.InvitationStatus.Accepted;
        }

        if (dbResponse.State != EntityState.Modified && dbResponse.State != EntityState.Deleted)
            throw new Exception("Database fail");

      
        
        // check for conflict in invitations, or remaining invitation from opposite user, remove opposite invitation if necessary
        var oppositeInvitation = await FriendsDataAccess.FindInvitation(userId, senderId);

        if (oppositeInvitation != null)
        {
            dbResponse = Context.Invitations.Remove(oppositeInvitation);
            if(dbResponse.State != EntityState.Deleted)
                throw new Exception("Database fail");
        }
        
        
        await Context.SaveChangesAsync();
        
        return invitationStatus;
    }

    public async Task<ResponseModel> GetAllInvitations(string userId)
    {
        var invitations = FriendsDataAccess.GetAllAvailableInvitationsSenders(userId);
        
        if (invitations == null || invitations.Count == 0)
            return ResponseModel.CreateResponse(FriendsResponseResult.Fail, "No invitations");

        var invitationsDtoList = new List<InvitationDto>();
        
        foreach (var sender in invitations)
        {
            invitationsDtoList.Add(new InvitationDto(sender.UserName));
        }

        var result = JsonConvert.SerializeObject(invitationsDtoList);
        
        return ResponseModel.CreateResponse(FriendsResponseResult.Success, result);
    }
    
    
    private class InvitationDto
    {
        public string Sender;

        public InvitationDto(string sender)
        {
            Sender = sender;
        }
    }
}