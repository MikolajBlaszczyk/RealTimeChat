using Microsoft.EntityFrameworkCore;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;
using RealTimeChat.BusinessLogic.FriendsLogic.Models;
using RealTimeChat.BusinessLogic.FriendsLogic;
using RealTimeChat.BusinessLogic.FriendsLogic.Enums;
using RealTimeChat.BusinessLogic.FriendsLogic.FriendsManagerDir;

namespace RealTimeChat.BusinessLogic.FriendsLogic;

public class FriendsRequestHandler : IFriendsRequestHandler
{
    public ApplicationContext Context { get; }
    public IFriendsManager FriendsManager { get; }


    public FriendsRequestHandler(ApplicationContext context, IFriendsManager friendsManager)
    {
        Context = context;
        FriendsManager = friendsManager;
    }
    public async Task<ResponseModel> AddFriend(string UserId, string FriendUsername)
    {
        var friend = await Context.Users.FirstOrDefaultAsync(u => u.UserName == FriendUsername);
        if (friend == null)
            return ResponseModel.CreateResponse(FriendsResponseResult.Fail, "User does not exist");

        string friendId = friend.Id;
        var user = await Context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        var result = await FriendsManager.AddFriend(UserId, friendId);

        return result;
    }

    public Task<ResponseModel> GetAllFriends(string UserId, string FriendId)
    {
        throw new NotImplementedException();
    }
}