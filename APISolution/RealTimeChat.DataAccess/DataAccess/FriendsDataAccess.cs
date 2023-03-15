using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Interfaces;
using RealTimeChat.DataAccess.Models;


namespace RealTimeChat.DataAccess.DataAccess;

public class FriendsDataAccess : IFriendsDataAccess
{
    private readonly ApplicationContext Context;
    public readonly IUserUtils Utils;

    public FriendsDataAccess(ApplicationContext context, IUserUtils utils)
    {
        Context = context;
        Utils = utils;
    }

    public async Task<FriendsModel?> FindFriendship(string userId, string friendId)
    {
        var friendship = await Context.Friends.FirstOrDefaultAsync(f =>
            f.UserId == userId && f.FriendId == friendId ||
            f.UserId == friendId && f.FriendId == userId
        );
        return friendship;
    }

    public async Task<InvitationModel?> FindInvitation(string senderId, string responderId)
    {
        var invitation = await Context.Invitations.FirstOrDefaultAsync(i =>
            i.SenderId == senderId && i.ResponderId == responderId 
        );
        return invitation;
    }

    public async Task<List<InvitationModel>?> FindInvitationsBothSides(string senderId, string responderId)
    {
        var invitation =  Context.Invitations.Where(i =>
            i.SenderId == senderId && i.ResponderId == responderId ||
            i.SenderId == responderId && i.ResponderId == senderId 
        ).ToList();
        return invitation;
    }

    public List<ApplicationUser>? GetAllFriendsUsers(string userId)
    {
        var friends = Context.Friends
            .Include(u => u.Friend)
            .Include(u => u.User)
            .Include(u => u.User.Status)
            .Include(u => u.Friend.Status)
            .Include(u => u.User.ThisSession)
            .Include(u => u.Friend.ThisSession)
            .Where(u => u.UserId == userId || u.FriendId == userId).ToList();

        var usersList = new List<ApplicationUser>();

        foreach (var friend in friends)
        {
            var friendId = friend.User.Id;
            if (friendId != userId)
            {
                usersList.Add(friend.User);
            }
            else
            {
                usersList.Add(friend.Friend);
            }
        }

        return usersList;
    }

    public List<ApplicationUser>? GetAllAvailableInvitationsSenders(string userId)
    {
        var invitations = Context.Invitations
            .Include(i => i.Sender)
            .Where(i => i.ResponderId == userId)?.ToList();
        
        if (invitations == null)
            return null;
        
        var senders = new List<ApplicationUser>();

        foreach (var invitation in invitations)
        {
            if(invitation.Status != "Declined")
                senders.Add(invitation.Sender);
        }

        return senders;
    }

    public async Task<string> GetFriendGuidByUserName(string username, string userId)
    {
        if (username == string.Empty)
            throw new ArgumentException("Empty username");
        
        var friendId = await Utils.GetGuidByUserName(username);

        if (friendId == null)
            throw new ArgumentException("User does not exist");
        if (friendId == userId)
            throw new ArgumentException("Invalid user");

        return friendId;
    }
}