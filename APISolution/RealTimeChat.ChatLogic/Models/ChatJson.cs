using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.ChatLogic.Models
{
    public class ChatJson
    {
        public string MessagesJson { get;  set; }

        public static ChatJson CreateChatJSON(string json)
        {
            return new ChatJson { MessagesJson = json };
        }
    }
}
