using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.Interfaces;

public interface IFriendsDataAccess
{
    Task<FriendsModel?> FindFriendship(string userId, string friendId);
    Task<InvitationModel?> FindInvitation(string senderId, string responderId);
    Task<List<InvitationModel>?> FindInvitationsBothSides(string senderId, string responderId);
    List<ApplicationUser>? GetAllFriendsUsers(string userId);
    List<ApplicationUser>? GetAllAvailableInvitationsSenders(string userId);
    Task<string> GetFriendGuidByUserName(string username, string userId);
}