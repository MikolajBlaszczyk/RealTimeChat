using Microsoft.Extensions.Logging;
using RealTimeChat.ChatLogic.ChatRetention;
using RealTimeChat.DataAccess.KeyDataAccess;

namespace RealTimeChat.ChatLogic
{
    public class ChatPersister
    {
        public MessageDataAccess DataAccess { get; }
        private ILogger<ChatPersister> Loger { get; }

        public ChatPersister(MessageDataAccess dataAccess, ILogger<ChatPersister> loger)
        {
            DataAccess = dataAccess;
            Loger = loger;
        }

        public ChatResponseModel Save(string connectionID_A, string connectionID_B,string messages_A, string messages_B)
        {
            
        }

    }
}
