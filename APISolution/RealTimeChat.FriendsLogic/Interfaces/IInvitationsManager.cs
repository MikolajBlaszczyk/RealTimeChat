using RealTimeChat.FriendsLogic.Models;
using InvitationStatus = RealTimeChat.FriendsLogic.Enums.InvitationStatus;

namespace RealTimeChat.FriendsLogic.Interfaces;

public interface IInvitationsManager
{
    Task<ResponseModel> CreateInvitation(string userId, string friendId);
    Task<InvitationStatus> UpdateInvitation(string friendUsername, string userId, bool response);
    Task<ResponseModel> GetAllInvitations(string userId);
}