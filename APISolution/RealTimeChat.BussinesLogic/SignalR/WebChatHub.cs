using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using RealTimeChat.BusinessLogic.WebSupervisors;
using System.Threading;
using RealTimeChat.BusinessLogic.AvailabilityManager;
using RealTimeChat.ChatLogic;
using RealTimeChat.DataAccess.DataAccess;


namespace RealTimeChat.SignalR;

[Authorize]
public class WebChatHub:Hub
{
    public UserConnectionHandler Handler { get; }
    public ChatPersister Persister { get; }
    public StatusManager StatusManager { get; }

    public WebChatHub(UserConnectionHandler handler, ChatPersister persister, StatusManager statusManager)
    {
        Handler = handler;
        Persister = persister;
        StatusManager = statusManager;
    }

    public override async Task OnConnectedAsync()
    {
        await Handler.HandleUserConnection(Context);
        
        await base.OnConnectedAsync();

        await StatusManager.UpdateStatus(Context, "Online");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Handler.HandleUserDisconnection(Context);

        await base.OnDisconnectedAsync(exception);

        await StatusManager.UpdateStatus(Context, "Offline");
    }

  
    public Task SendMessageTo(string userName, string messageContent, string connectionId)
    {
        Task.Run(() => Persister.Save(Context.ConnectionId, messageContent, connectionId)); 

        return Clients.Client(connectionId).SendAsync("MessageFromOtherClient", userName, messageContent);
    }
}