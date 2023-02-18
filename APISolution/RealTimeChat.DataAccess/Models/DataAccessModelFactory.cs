using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.Models;

public static class DataAccessModelFactory
{
    public static Session CreateSessionModel(string guid)
    {
        return new Session()
        {
            UserGUID = guid,
            SignInDate = DateTime.Now
        };
    }
}