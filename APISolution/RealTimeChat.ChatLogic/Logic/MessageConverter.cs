using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.ChatLogic.Logic
{
    public class MessageConverter
    {


        public string ConvertToJson(string username, string message)
        {
            return $"{username}:{message}";
        }
    }
}
