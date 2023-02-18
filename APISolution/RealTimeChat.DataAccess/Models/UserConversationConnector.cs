using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RealTimeChat.DataAccess.Models
{
    public class UserConversationConnector
    {

        public string UserGUID { get; set; }
        public int ConversationID { get; set; }

        public UserIdentityExtended User { get; set; }
        public Conversation  Conversation{ get; set; }

    }
}
