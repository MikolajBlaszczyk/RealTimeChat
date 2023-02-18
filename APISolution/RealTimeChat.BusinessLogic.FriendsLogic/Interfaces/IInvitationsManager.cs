using RealTimeChat.BusinessLogic.FriendsLogic.Enums;
using RealTimeChat.BusinessLogic.FriendsLogic.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;

public interface IInvitationsManager
{
    Task<InvitationStatus> CreateInvitation(string UserId, string FriendId);
    Task<InvitationStatus> UpdateInvitation(string UserId, string FriendId, bool Response);
}