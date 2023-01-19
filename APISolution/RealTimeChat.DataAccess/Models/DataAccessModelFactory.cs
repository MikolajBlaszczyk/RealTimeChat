namespace RealTimeChat.API.DataAccess.Models;

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