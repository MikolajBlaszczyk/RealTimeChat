using Microsoft.Extensions.Logging;
using RealTimeChat.ChatLogic.ChatRetention;

namespace RealTimeChat.ChatLogic
{
    public class SavingChatManager
    {
        private ILogger<SavingChatManager> Loger { get; }

        public SavingChatManager(ILogger<SavingChatManager> loger)
        {
            Loger = loger;
        }

        public ChatResponseModel Save(string messages)
        {
            return null;
        }

    }
}
