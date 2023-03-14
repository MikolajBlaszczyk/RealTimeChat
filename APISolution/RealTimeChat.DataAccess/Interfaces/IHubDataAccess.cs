namespace RealTimeChat.DataAccess.Interfaces;

public interface IHubDataAccess
{
    Task UpdateSessionConnection(string guid, string connectionID);
    Task DeleteSessionConnection(string guid);
}