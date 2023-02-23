using RealTimeChat.FriendsLogic.Models;

namespace RealTimeChat.FriendsLogic.Interfaces;

public interface IFriendsRequestHandler
{
    Task<ResponseModel> AddFriend(string userId, string friendUsername);
    Task<ResponseModel> GetAllFriends(string userId);
    Task<ResponseModel> RemoveFriend(string userId, string friendUsername);

    Task<ResponseModel> GetAllInvitations(string userId);
    Task<ResponseModel> InvitationResponse(string userId, string friendUsername, bool response);
}