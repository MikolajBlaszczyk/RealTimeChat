using RealTimeChat.BusinessLogic.FriendsLogic.Enums;
using RealTimeChat.BusinessLogic.FriendsLogic.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;

public interface IInvitationsManager
{
    Task<ResponseModel> CreateInvitation(string userId, string friendId);
    Task<InvitationStatus> UpdateInvitation(string friendUsername, string userId, bool response);
    Task<ResponseModel> GetAllInvitations(string userId);
}