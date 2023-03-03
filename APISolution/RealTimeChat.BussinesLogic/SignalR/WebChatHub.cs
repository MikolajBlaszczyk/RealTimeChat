using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using RealTimeChat.BusinessLogic.WebSupervisors;
using System.Threading;
using RealTimeChat.ChatLogic;
using RealTimeChat.DataAccess.DataAccess;


namespace RealTimeChat.SignalR;

[Authorize]
public class WebChatHub:Hub
{
    public UserConnectionHandler Handler { get; }
    public ChatPersister Persister { get; }

    public WebChatHub(UserConnectionHandler handler, ChatPersister persister)
    {
        Handler = handler;
        Persister = persister;
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

  
    public Task SendMessageTo(string userName, string messageContent, string connectionId)
    {
        Task.Run(() => Persister.Save(Context.ConnectionId, messageContent, connectionId)); 

        return Clients.Client(connectionId).SendAsync("MessageFromOtherClient", userName, messageContent);
    }
}