using Microsoft.AspNetCore.Identity;
using RealTimeChat.API.Models;
using RealTimeChat.BusinessLogic.UserAvaliability;

namespace RealTimeChat.API.Controllers;

public class ChatManager
{
    private readonly IAvailablilityManager AvailabilityManager;
    private readonly IHttpContextAccessor Accessor;

    public ChatManager(IAvailablilityManager availabilityManager,IHttpContextAccessor accessor)
    {
        AvailabilityManager = availabilityManager;
        Accessor = accessor;
    }

    public async Task RegisterAvailableUser(IdentityUser availableUser)
    {
        await AvailabilityManager.AddAvailableUser(availableUser.UserName);
    }

    public async Task<bool> UpdateConnectionInformation(ConnectionModel model, string userName)
    {
        
        var result = await AvailabilityManager.UpdateAvailableUserInformation(userName, model.ConnectionID);

        return result;
    }
}