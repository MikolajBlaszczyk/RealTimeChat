namespace RealTimeChat.DataAccess.Interfaces;

public interface IStatusDataAccess
{
    Task UpdateUserStatus(string guid, string newStatus);
    Task<List<string>> GetFriendsConnectionIds(string guid);
}