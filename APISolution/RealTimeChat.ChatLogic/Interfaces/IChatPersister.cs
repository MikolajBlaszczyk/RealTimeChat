using RealTimeChat.ChatLogic.ChatRetention;

namespace RealTimeChat.ChatLogic.Interfaces;

public interface IChatPersister
{
    Task<ChatResponseModel> Save(string connectionID_A, string messages_A, string connectionID_B);
}