using RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;
using RealTimeChat.BusinessLogic.FriendsLogic.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic;

public class UserFriendsRequestHandler : IUserFriendsRequestHandler
{
    public Task<ResponseModel> AddFriend(string UserId, string FriendId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel> GetAllFriends(string UserId, string FriendId)
    {
        throw new NotImplementedException();
    }
}