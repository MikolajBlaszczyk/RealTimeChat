using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.DataAccess.Models;
using RealTimeChat.BusinessLogic.FriendsLogic.Enums;
using RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;
using RealTimeChat.BusinessLogic.FriendsLogic.Models;


namespace RealTimeChat.BusinessLogic.FriendsLogic.FriendsManagerDir;

public class FriendsManager : IFriendsManager
{
    public ApplicationContext Context { get; }
    public IInvitationsManager InvitationsManager { get; set; }
    public IDbUserHelper DbUserHelper { get; }

    public FriendsManager(ApplicationContext context, IInvitationsManager invitationsManager, IDbUserHelper dbUserHelper)
    {
        Context = context;
        InvitationsManager = invitationsManager;
        DbUserHelper = dbUserHelper;
    }
    
    public async Task<ResponseModel> AddFriend(string userId, string friendId)
    {
        var friendship = await DbUserHelper.FindFriendship(userId, friendId);
        
        if (friendship != null)
            return ResponseModel.CreateResponse(FriendsResponseResult.AlreadyFriend, "Already befriended.");

        var invitation = await DbUserHelper.FindBothSidesInvitation(userId, friendId);
        if (invitation != null)
            return ResponseModel.CreateResponse(FriendsResponseResult.AlreadyFriend, "Invitation pending");

        // create invitation
        var status = await InvitationsManager.CreateInvitation(userId, friendId);

        return status;
    }

    public async Task<ResponseModel> CreateFriendship(string userId, string friendId)
    {
        var finder = await DbUserHelper.FindFriendship(userId, friendId);

        if (finder != null)
            return ResponseModel.CreateResponse(FriendsResponseResult.AlreadyFriend, "Already friend");

        FriendsModel model = new FriendsModel();
        model.UserId = userId;
        model.FriendId = friendId;
        var dbResponse = await Context.Friends.AddAsync(model);

        if (dbResponse.State == EntityState.Added)
        {
            await Context.SaveChangesAsync();
            return ResponseModel.CreateResponse(FriendsResponseResult.Success, "Friend added");
        }

        return ResponseModel.CreateResponse(FriendsResponseResult.ServerError, "Internal error");
    }

    public async Task<ResponseModel> GetAllFriends(string userId)
    {
        var friends = DbUserHelper.GetAllFriendsUsers(userId);
         
      
        if (friends == null || friends.Count == 0)
            return ResponseModel.CreateResponse(FriendsResponseResult.Success, "You have no friends");


        var friendsDtoList = new List<UserDto>();

        foreach (var friend in friends)
        {
            friendsDtoList.Add(new UserDto(friend.UserName, "default"));
        }

        var response = JsonConvert.SerializeObject(friendsDtoList);

        return ResponseModel.CreateResponse(FriendsResponseResult.Success, response);
    }

    public async Task<ResponseModel> RemoveFriend(string userId, string friendId)
    {
        var friends = DbUserHelper.GetAllFriendsUsers(userId);

        var friend = friends?.FirstOrDefault(f => f.Id == friendId);

        if (friend == null)
            return ResponseModel.CreateResponse(FriendsResponseResult.InvalidUser, "Invalid friend");

        var friendToRemove = await DbUserHelper.FindFriendship(userId, friendId);
        
        if(friendToRemove == null)
            return ResponseModel.CreateResponse(FriendsResponseResult.ServerError, "Internal error");
        
        var dbResult = Context.Friends.Remove(friendToRemove);

        if (dbResult.State == EntityState.Deleted)
        {
            await Context.SaveChangesAsync();
            return ResponseModel.CreateResponse(FriendsResponseResult.Success, "Friend removed");
        }

        return ResponseModel.CreateResponse(FriendsResponseResult.ServerError, "Internal error");
    }

    private class UserDto
    {
        public string Username;
        public string ConnectionId;

        public UserDto(string username, string connectionId)
        {
            Username = username;
            ConnectionId = connectionId;
        }
    }
}