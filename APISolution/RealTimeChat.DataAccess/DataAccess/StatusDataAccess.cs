using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess.IdentityContext;

namespace RealTimeChat.DataAccess.DataAccess;

public class StatusDataAccess
{
    private ApplicationContext DbContext { get; }

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
        var friends = DbContext.Friends
            .Include(friendsModel => friendsModel.User)
                .ThenInclude(user => user.ThisSession)
            .Include(friendsModel => friendsModel.Friend)
                .ThenInclude(friend => friend.ThisSession)
            .Where(friendsModel => friendsModel.UserId == guid || friendsModel.FriendId == guid)
            .ToList();

     

        var activeFriends = new List<string>();

        foreach (var friend in friends)
        {
            try
            {
                if (friend.UserId != guid && friend.User.ThisSession.ConnectionID != null)
                {
                    activeFriends.Add(friend.User.ThisSession.ConnectionID);
                }
            
                else if (friend.FriendId != guid && friend.Friend.ThisSession.ConnectionID != null)
                {
                    activeFriends.Add(friend.Friend.ThisSession.ConnectionID!);
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        return activeFriends;
    }
}