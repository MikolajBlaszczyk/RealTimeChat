using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using RealTimeChat.BusinessLogic.WebSupervisors;
using System.Threading;
using RealTimeChat.DataAccess.DataAccess;


namespace RealTimeChat.SignalR;

[Authorize]
public class WebChatHub:Hub
{
    public UserConnectionHandler Handler { get; }

    public WebChatHub(UserConnectionHandler handler, ChatPersister persister)
    {
        Handler = handler;
    }

    public override async Task OnConnectedAsync()
    {
        await Handler.HandleUserConnection(Context);
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Handler.HandleUserDisconnection(Context);

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