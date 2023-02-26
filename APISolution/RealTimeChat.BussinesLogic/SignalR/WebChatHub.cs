using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using RealTimeChat.DataAccess.DataAccess;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.SignalR;

[Authorize]
public class WebChatHub:Hub
{
    private readonly IHttpContextAccessor _context;
    public HubDataAccess DataAccess { get; }
    public UserManager<ApplicationUser> UserManager { get; }

    public WebChatHub(HubDataAccess dataAccess, UserManager<ApplicationUser> userManager)
    {
        DataAccess = dataAccess;
        UserManager = userManager;
    }

    public override Task OnConnectedAsync()
    {
        string? guid = Context.User.FindFirst("GUID")?.Value;
        string? connectionID = Context.ConnectionId;

        DataAccess.UpdateSessionConnection(guid, connectionID);

        return base.OnConnectedAsync();
    }

    public override  Task OnDisconnectedAsync(Exception? exception)
    {
        string? guid = Context.User.FindFirst("GUID")?.Value;

        if(guid is null)
            throw new Exception();

        DataAccess.DeleteSessionConnection(guid);
        return base.OnDisconnectedAsync(exception);
    }

    public Task SendMessageToAll(string userName, string messageContent)
    {
        return Clients.All.SendAsync("GetMessage",userName, messageContent);
    }

    public Task SendMessageTo(string userName, string messageContent, string connectionId)
    {
        return Clients.Client(connectionId).SendAsync("MessageFromOtherClient", userName, messageContent);
    }
}