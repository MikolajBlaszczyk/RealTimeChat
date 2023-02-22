using RealTimeChat.BusinessLogic.FriendsLogic.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;

public interface IFriendsManager
{
    Task<ResponseModel> AddFriend(string userId, string friendUsername);
    Task<ResponseModel> CreateFriendship(string userId, string friendUsername);

    Task<ResponseModel> GetAllFriends(string userId);
    Task<ResponseModel> RemoveFriend(string userId, string friendUsername);

}