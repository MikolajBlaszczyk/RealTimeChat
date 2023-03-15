using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.Interfaces;

public interface ISessionUtils
{
    Task<Session> GetSessionByUserGuid(string userGuid);
    Task UpdateSession(Session session);
    Task DeleteSession(Session session);
}