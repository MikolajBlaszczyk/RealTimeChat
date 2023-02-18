using RealTimeChat.BusinessLogic.FriendsLogic.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;

public interface IFriendsRequestHandler
{
    Task<ResponseModel> AddFriend(string UserId, string FriendId);
    Task<ResponseModel> GetAllFriends(string UserId, string FriendId);
}