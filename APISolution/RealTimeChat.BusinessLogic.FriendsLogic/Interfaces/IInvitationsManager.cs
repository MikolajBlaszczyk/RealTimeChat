using RealTimeChat.BusinessLogic.FriendsLogic.Enums;
using RealTimeChat.BusinessLogic.FriendsLogic.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;

public interface IInvitationsManager
{
    Task<ResponseModel> CreateInvitation(string userId, string friendId);
    Task<ResponseModel> UpdateInvitation(string userId, string friendId, bool response);
    Task<ResponseModel> GetAllInvitations(string userId);
}