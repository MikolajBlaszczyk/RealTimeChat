

namespace RealTimeChat.DataAccess.Models
{
    public class Conversation
    {
        public int ID { get; set; }
        public int Part { get; set; }
        public string Message { get; set; }

        public ICollection<UserConversationConnector> Connectors { get; set; }
    }
}
