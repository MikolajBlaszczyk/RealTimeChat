using RealTimeChat.API.DataAccess.Models;

namespace RealTimeChat.DataAccess.Models
{
    public class UserConversationConnector
    {

        public string UserGUID { get; set; }
        public int ConversationID { get; set; }

        public ApplicationUser User { get; set; }
        public Conversation  Conversation{ get; set; }

    }
}
