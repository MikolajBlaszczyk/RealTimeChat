using RealTimeChat.API.DataAccess.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;

public interface IDbUserHelper
{
    public Task<string?> UserUsernameToId(string username);
    public Task<ApplicationUser?> FindUser(string userId);
    public Task<FriendsModel?> FindFriendship(string userId, string friendId);
    public Task<InvitationModel?> FindInvitation(string senderId, string responderId);

    public Task<InvitationModel?> FindBothSidesInvitation(string senderId, string responderId);
    public Task<List<InvitationModel>?> FindBothSidesInvitations(string senderId, string responderId);
    public List<ApplicationUser>? GetAllFriendsUsers(string userId);
    public List<ApplicationUser>? GetAllAvailableInvitationsSenders(string userId);

    public Task<string> FriendUsernameToId(string username, string userId);
}