namespace RealTimeChat.BusinessLogic.UserAvaliability;

public interface IAvailablilityManager
{
    Task RemoveAvailableuser(string userName);
    Task AddAvailableUser(string userName);
    Task<bool> UpdateAvailableUserInformation(string userName, string connectionID);
}