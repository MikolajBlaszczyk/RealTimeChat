using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class WebChatHub:Hub
    {
        public Task SendMessage(string userName, string messageContent)
        {
            return Clients.All.SendAsync("GetMessage",userName, messageContent);
        }

        public Task GetAllUsers()
        {
            
        }
    }
}
