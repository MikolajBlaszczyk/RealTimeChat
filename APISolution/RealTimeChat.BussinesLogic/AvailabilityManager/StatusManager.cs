using Microsoft.AspNetCore.SignalR;
using RealTimeChat.DataAccess.Interfaces;

namespace RealTimeChat.BusinessLogic.AvailabilityManager;

public class StatusManager
{
    private const string GuidClaim = "GUID";
    private IStatusDataAccess DataAccess { get; }

    public StatusManager(IStatusDataAccess dataAccess)
    {
        DataAccess = dataAccess;
    }
    
    public async Task UpdateStatus(HubCallerContext context, string newStatus)
    {
        var guid = context.User.FindFirst(GuidClaim)?.Value;

        if(guid == null)
            return;
        
        await DataAccess.UpdateUserStatus(guid, newStatus);
    }

    public async Task<List<string>> GetActiveConnections(HubCallerContext context)
    {
        var guid = context.User.FindFirst(GuidClaim)?.Value;
        
        if(guid == null)
            return new List<string>();
        
        return await DataAccess.GetFriendsConnectionIds(guid);
    }
}