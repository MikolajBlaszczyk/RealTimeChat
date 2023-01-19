using Microsoft.AspNetCore.SignalR;

namespace RealTimeChat.SignalR;

public class WebChatHub:Hub
{
    public Task SendMessageToAll(string userName, string messageContent)
    {
        return Clients.All.SendAsync("GetMessage",userName, messageContent);
    }

    public Task SendMessageTo(string userName, string messageContent, string connectionId)
    {
        return Clients.Client(connectionId).SendAsync("MessageFromOtherClient", userName, messageContent);
    }
}