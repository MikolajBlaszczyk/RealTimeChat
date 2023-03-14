namespace RealTimeChat.DataAccess.Interfaces;

public interface IAccountDataAccess
{
    Task<string?> GetUserGuid(string username);
}