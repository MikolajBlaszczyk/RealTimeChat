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
}