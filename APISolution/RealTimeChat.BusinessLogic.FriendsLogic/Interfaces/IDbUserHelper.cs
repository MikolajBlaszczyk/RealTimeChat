using RealTimeChat.API.DataAccess.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;

public interface IDbUserHelper
{
    public Task<string?> FindUserUsername(string username);
    public Task<ApplicationUser?> FindUser(string userId);
    public Task<FriendsModel?> FindFriendship(string userId, string friendId);
    public Task<InvitationModel?> FindInvitation(string senderId, string responderId);

    public Task<InvitationModel?> FindBothSidesInvitation(string senderId, string responderId);
    public List<ApplicationUser>? GetAllFriendsUsers(string userId);
    public List<ApplicationUser>? GetAllInvitationsSenders(string userId);
}