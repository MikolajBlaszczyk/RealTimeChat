using RealTimeChat.ChatLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.ChatLogic.ModelsFactory
{
    public class ChatJsonFactory
    {
        public ChatJson CreateChatJSON(string json)
        {
            return new ChatJson { MessagesJson =  json };
        }
    }
}
