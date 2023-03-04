using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Models;
using RealTimeChat.FriendsLogic.Enums;
using RealTimeChat.FriendsLogic.Interfaces;
using RealTimeChat.FriendsLogic.Models;


namespace RealTimeChat.FriendsLogic.FriendsManagers;

public class FriendsManager : IFriendsManager
{
    private ApplicationContext Context { get; }
    private IInvitationsManager InvitationsManager { get; }
    private IDbUserHelper DbUserHelper { get; }

    public FriendsManager(ApplicationContext context, IInvitationsManager invitationsManager, IDbUserHelper dbUserHelper)
    {
        Context = context;
        InvitationsManager = invitationsManager;
        DbUserHelper = dbUserHelper;
    }
    
    public async Task<ResponseModel> AddFriend(string userId, string friendUsername)
    {
  
        var friendId = await DbUserHelper.FriendUsernameToId(friendUsername, userId);
        
        var friendship = await DbUserHelper.FindFriendship(userId, friendId);
        
        
        if (friendship != null)
            return ResponseModel.CreateResponse(FriendsResponseResult.AlreadyFriend, "Already befriended.");


        var invitations = await DbUserHelper.FindBothSidesInvitations(userId, friendId);

        if (invitations != null)
        {
            foreach (var invitation in invitations)
            {
                if (invitation.SenderId == userId && invitation.Status is "Pending" or "Declined" ||
                    invitation.SenderId == friendId && invitation.Status == "Pending")
                {
                    return ResponseModel.CreateResponse(FriendsResponseResult.AlreadyFriend, "Invitation pending");
                }
            }
        }

        // create invitation
        var status = await InvitationsManager.CreateInvitation(userId, friendId);
        return status;
    }

    public async Task<ResponseModel> CreateFriendship(string userId, string friendUsername)
    {
        var friendId =  await DbUserHelper.FriendUsernameToId(friendUsername, userId);
        
        var friendship = await DbUserHelper.FindFriendship(userId, friendId);

        if (friendship != null)
            return ResponseModel.CreateResponse(FriendsResponseResult.AlreadyFriend, "Already friend");


        FriendsModel newFriendship = new FriendsModel()
        {
            UserId = userId,
            FriendId = friendId
        };
        
        var dbResponse = await Context.Friends.AddAsync(newFriendship);

        if (dbResponse.State == EntityState.Added)
        {
            await Context.SaveChangesAsync();
            return ResponseModel.CreateResponse(FriendsResponseResult.Success, "Friend added");
        }
        else
        {
            throw new Exception("Database fail");
        }
        
    }

    public async Task<ResponseModel> GetAllFriends(string userId)
    {
        var friends = DbUserHelper.GetAllFriendsUsers(userId);
        
        if (friends == null || friends.Count == 0)
            return ResponseModel.CreateResponse(FriendsResponseResult.Fail, "You have no friends");


        var friendsDtoList = new List<UserDto>();

        foreach (var friend in friends)
        {
            if (friend.ThisSession != null)
                friendsDtoList.Add(new UserDto(
                    friend.UserName, friend.Status.StatusName, friend.ThisSession.ConnectionID
                    ));
            else
            {
                friendsDtoList.Add(new UserDto(friend.UserName, friend.Status.StatusName, "null"));
            }
        }

        var result = JsonConvert.SerializeObject(friendsDtoList);

        return ResponseModel.CreateResponse(FriendsResponseResult.Success, result);
    }

    public async Task<ResponseModel> RemoveFriend(string userId, string friendUsername)
    {
        var friendId =  await DbUserHelper.FriendUsernameToId(friendUsername, userId);
        
        var friendship = await DbUserHelper.FindFriendship(userId, friendId);
        
        if(friendship == null)
            return ResponseModel.CreateResponse(FriendsResponseResult.Fail, "You are not friend with this user");
        
        var dbResult = Context.Friends.Remove(friendship);

        if (dbResult.State != EntityState.Deleted)
            throw new Exception("Internal error");
        
        await Context.SaveChangesAsync();
        return ResponseModel.CreateResponse(FriendsResponseResult.Success, "Friend removed");

    }



    private class UserDto
    {
        public string Username;
        public string Status;
        public string ConnectionId;

        public UserDto(string username, string status, string connectionId = "Default")
        {
            Username = username;
            ConnectionId = connectionId;
            Status = status;
        }
    }
}