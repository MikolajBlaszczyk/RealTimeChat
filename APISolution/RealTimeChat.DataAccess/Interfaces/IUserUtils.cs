using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.Interfaces;

public interface IUserUtils
{
    Task<ApplicationUser?> GetUserByUserName(string userName);
    Task<ApplicationUser?> GetUserByConnection(string connectionID);
    Task<string?> GetGuidByUserName(string username);
}