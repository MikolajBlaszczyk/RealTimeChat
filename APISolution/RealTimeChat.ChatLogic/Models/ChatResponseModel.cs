using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.ChatLogic.Models
{
    public class ChatResponseModel
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public static ChatResponseModel CreateChatResponse(bool success, string message = "")
        {
            return new ChatResponseModel { Success = success, Message = message };
        }
    }
}
