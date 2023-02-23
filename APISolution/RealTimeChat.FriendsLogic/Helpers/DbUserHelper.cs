using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Models;
using RealTimeChat.FriendsLogic.Enums;
using RealTimeChat.FriendsLogic.Models;
using RealTimeChat.FriendsLogic.Interfaces;

namespace RealTimeChat.FriendsLogic.Helpers;

public class DbUserHelper : IDbUserHelper
{
    private ApplicationContext Context { get; }

    public DbUserHelper(ApplicationContext context)
    {
        Context = context;
    }

    public async Task<string?> UserUsernameToId(string username)
    {
        var user = await Context.Users.FirstOrDefaultAsync(u => u.UserName == username);

        return user?.Id;
    }
    
    public async Task<ApplicationUser?> FindUser(string userId)
    {
        var user = await Context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        return user;
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

    public async Task<InvitationModel?> FindBothSidesInvitation(string senderId, string responderId)
    {
        var invitation = await Context.Invitations.FirstOrDefaultAsync(i =>
            i.SenderId == senderId && i.ResponderId == responderId ||
            i.SenderId == responderId && i.ResponderId == senderId 
        );
        return invitation;
    }
    
    public async Task<List<InvitationModel>?> FindBothSidesInvitations(string senderId, string responderId)
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

    public async Task<string> FriendUsernameToId(string username, string userId)
    {
        if (username == string.Empty)
            throw new ArgumentException("Empty username");
        
        var friendId = await UserUsernameToId(username);

        if (friendId == null)
            throw new ArgumentException("User does not exist");
        if (friendId == userId)
            throw new ArgumentException("Invalid user");

        return friendId;
    }
}