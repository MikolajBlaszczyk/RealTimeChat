using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.Interfaces;

public interface IConversationUtils
{
    Task<Conversation?> GetConversationByUsers(string userGuid_A, string userGuid_B);
    Task SaveConversation(Conversation conversation);
}