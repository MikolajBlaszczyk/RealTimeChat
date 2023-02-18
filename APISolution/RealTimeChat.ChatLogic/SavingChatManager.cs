using Microsoft.Extensions.Logging;
using RealTimeChat.ChatLogic.ChatRetention;

namespace RealTimeChat.ChatLogic
{
    public class SavingChatManager
    {
        private ILogger<SavingChatManager> Looger { get; }

        public SavingChatManager(ILogger<SavingChatManager> looger)
        {
            Looger = looger;
        }

        public ChatResponseModel Save(string messages)
        {
            return null;
        }

    }
}
