using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.Interfaces;

public interface IChatPersisterDataAccess
{
    Task<Conversation> GetConversation(string connectionID_A, string connectionID_B);
    Task<string> GetUsername(string connectionID);
    Task SaveConversation(Conversation conversation);
}