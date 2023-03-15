using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RealTimeChat.BusinessLogic.AvailabilityManager;
using RealTimeChat.BusinessLogic.ChatSupervisors;
using RealTimeChat.ChatLogic.Interfaces;

namespace RealTimeChat.BusinessLogic.SignalR;

[Authorize]
public class WebChatHub:Hub
{
    public UserConnectionHandler Handler { get; }
    public IChatPersister Persister { get; }
    public StatusManager StatusManager { get; }

    public WebChatHub(UserConnectionHandler handler, IChatPersister persister, StatusManager statusManager)
    {
        Handler = handler;
        Persister = persister;
        StatusManager = statusManager;
    }

    public async Task SendGroupNotification(string status)
    {
        string username = Context.User.Identity.Name;
        string guid = Context.ConnectionId;
        
        var connectionIds = await StatusManager.GetActiveConnections(Context);

        await Clients.Clients(connectionIds).SendAsync("Notification", status,username, guid);
    }

    public async void SendOthers(string message)
    {
        await Clients.Others.SendAsync("ReceiveMessage", message);
    }
    
    public override async Task OnConnectedAsync()
    {
        await Handler.HandleUserConnection(Context);

        await StatusManager.UpdateStatus(Context, "Online");
        await SendGroupNotification("Online");
        
        await base.OnConnectedAsync();

    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Handler.HandleUserDisconnection(Context);

        await StatusManager.UpdateStatus(Context, "Offline");
        await SendGroupNotification("Offline");
        
        await base.OnDisconnectedAsync(exception);

    }

  
    public Task SendMessageTo(string userName, string messageContent, string connectionId)
    {
        Task.Run(() => Persister.Save(Context.ConnectionId, messageContent, connectionId)); 

        return Clients.Client(connectionId).SendAsync("MessageFromOtherClient", userName, messageContent);
    }
}