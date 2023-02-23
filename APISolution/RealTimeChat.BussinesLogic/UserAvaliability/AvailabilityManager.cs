using Microsoft.AspNetCore.SignalR;
using RealTimeChat.Models;
using RealTimeChat.SignalR;

namespace RealTimeChat.BusinessLogic.UserAvaliability;

public class AvailablilityManager : IAvailablilityManager
{
   private static object Locker = new object();
   private List<OnlineUserModel> OnlineUsers;
   private readonly IHubContext<WebChatHub> HubContext;

   public AvailablilityManager(IHubContext<WebChatHub> hubContext)
   {
      HubContext = hubContext;
      OnlineUsers = new List<OnlineUserModel>();
   }

   public async Task RemoveAvailableuser(string userName)
   {

      lock (Locker) 
      { 
         var offlineUser = OnlineUsers.FirstOrDefault(user => user.UserName == userName);
         if(offlineUser != null)
            OnlineUsers.Remove(offlineUser);
      }

      InformUsers();
   }
   
   public async Task AddAvailableUser(string userName)
   {
      var availableUser = new OnlineUserModel{ UserName = userName};
      lock (Locker)
      {
         OnlineUsers.Add(availableUser);
      }

      InformUsers();
   }

   public async Task<bool> UpdateAvailableUserInformation(string userName, string connectionID)
   {
       bool outcome = true;

       lock (Locker)
       {
           var index = OnlineUsers.FindIndex(user => user.UserName == userName);
           if (index != -1)
               OnlineUsers[index].ConnectionID = connectionID;
           else
               outcome = false;
       }

       return outcome;
   }

   public Task InformUsers()
   {
       var OnlineUsersNames = OnlineUsers.Select(user => user.UserName).ToList();

       return HubContext.Clients.All.SendAsync("AvailableUserInformation", OnlineUsersNames);
   }
}