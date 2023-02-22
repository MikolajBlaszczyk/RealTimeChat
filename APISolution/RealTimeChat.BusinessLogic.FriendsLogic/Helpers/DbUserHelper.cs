using Microsoft.EntityFrameworkCore;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.DataAccess.Models;
using RealTimeChat.BusinessLogic.FriendsLogic.Enums;
using RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;
using RealTimeChat.BusinessLogic.FriendsLogic.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.Helpers;

public class DbUserHelper : IDbUserHelper
{
    private ApplicationContext Context { get; }

    public DbUserHelper(ApplicationContext context)
    {
        Context = context;
    }

    public async Task<string?> FindUserUsername(string username)
    {
        var user = await Context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        
        if (user != null)
            return user.Id;
        
        return null;
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

    public List<ApplicationUser>? GetAllInvitationsSenders(string userId)
    {
        var invitations = Context.Invitations
            .Include(i => i.Sender)
            .Where(i => i.ResponderId == userId)?.ToList();
        
        if (invitations == null)
            return null;
        
        var senders = new List<ApplicationUser>();

        foreach (var model in invitations)
        {
            senders.Add(model.Sender);
        }

        return senders;
    }
}