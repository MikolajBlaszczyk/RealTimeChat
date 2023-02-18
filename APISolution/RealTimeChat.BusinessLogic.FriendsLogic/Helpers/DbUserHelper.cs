using Microsoft.EntityFrameworkCore;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.DataAccess.Models;
using RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;

namespace RealTimeChat.BusinessLogic.FriendsLogic.Helpers;

public class DbUserHelper : IDbUserHelper
{
    private ApplicationContext DbContext { get; }

    public DbUserHelper(ApplicationContext context)
    {
        DbContext = context;
    }

    public async Task<string?> FindUserUsername(string username)
    {
        var user = await DbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        
        if (user != null)
            return user.Id;
        
        return null;
    }
    
    public async Task<ApplicationUser?> FindUser(string userId)
    {
        var user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        return user;
    }

    public async Task<FriendsModel?> FindFriendship(string userId, string friendId)
    {
        var friendship = await DbContext.Friends.FirstOrDefaultAsync(f =>
            f.UserId == userId && f.FriendId == friendId ||
            f.UserId == friendId && f.FriendId == userId
        );
        return friendship;
    }

    public async Task<InvitationModel?> FindInvitation(string senderId, string responderId)
    {
        var invitation = await DbContext.Invitations.FirstOrDefaultAsync(i =>
            i.SenderId == senderId && i.ResponderId == responderId ||
            i.SenderId == responderId && i.ResponderId == senderId
        );
        return invitation;
    }
}