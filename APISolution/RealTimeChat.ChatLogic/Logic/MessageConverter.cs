using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChat.ChatLogic.Interfaces;

namespace RealTimeChat.ChatLogic.Logic
{
    public class MessageConverter : IMessageConverter
    {
        public string ConvertToJson(string username, string message)
        {
            return $"{username}:{message}";
        }
    }
}
