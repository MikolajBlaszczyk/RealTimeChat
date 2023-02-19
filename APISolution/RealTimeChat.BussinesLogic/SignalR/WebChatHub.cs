using Microsoft.AspNetCore.SignalR;
using RealTimeChat.DataAccess.DataAccess;

namespace RealTimeChat.SignalR;

public class WebChatHub:Hub
{
    public HubDataAccess DataAccess { get; }

    public WebChatHub(HubDataAccess dataAccess)
    {
        DataAccess = dataAccess;
    }

    public override  Task OnConnectedAsync()
    {
        string? guid = Context.User.FindFirst("GUID")?.Value;
        string? connectionID = Context.ConnectionId;

        if (guid is null || connectionID is null)
            throw new Exception();

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