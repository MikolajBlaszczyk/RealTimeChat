using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Interfaces;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.DataAccess;

public class StatusDataAccess : IStatusDataAccess
{
    private readonly ApplicationContext DbContext;

    public StatusDataAccess(ApplicationContext dbContext)
    {
        DbContext = dbContext;
    }
    
    public async Task UpdateUserStatus(string guid, string newStatus)
    {
        var user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id == guid);
        var statuses = DbContext.Statuses.ToList();

        if (user == null)
            return;

        foreach (var status in statuses.Where(status => status.StatusName == newStatus))
        {
            user.StatusId = status.StatusId;
            break;
        }

        await DbContext.SaveChangesAsync();
        
    }

    public async Task<List<string>> GetFriendsConnectionIds(string guid)
    {

        var friendships = DbContext.Friends
            .Where(f => f.UserId == guid || f.FriendId == guid)
            .Include(f => f.User)
            .ThenInclude(u => u.ThisSession)
            .Include(f => f.Friend)
            .ThenInclude(u => u.ThisSession).ToList();

        var friends = friendships
            .Select(f => f.UserId != guid ? f.User : f.Friend)
            .Where(u => u.ThisSession != null && u.ThisSession.ConnectionID != null)
            .Select(u => u.ThisSession.ConnectionID)
            .ToList();


        return friends;
    }
}