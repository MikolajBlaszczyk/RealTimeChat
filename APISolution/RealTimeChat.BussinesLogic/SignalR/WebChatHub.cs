using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using RealTimeChat.BusinessLogic.WebSupervisors;
using System.Threading;


namespace RealTimeChat.SignalR;

[Authorize]
public class WebChatHub:Hub
{
    public UserConnectionHandler Handler { get; }

    public WebChatHub(UserConnectionHandler handler)
    {
        Handler = handler;
    }

    public override async Task OnConnectedAsync()
    {
        await Handler.HandleUserConnection();
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Handler.HandleUserDisconnection();

        await base.OnDisconnectedAsync(exception);
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