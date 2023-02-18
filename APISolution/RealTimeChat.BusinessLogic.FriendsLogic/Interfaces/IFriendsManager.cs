using RealTimeChat.BusinessLogic.FriendsLogic.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;

public interface IFriendsManager
{
    Task<ResponseModel> AddFriend(string UserId, string FriendId);

}