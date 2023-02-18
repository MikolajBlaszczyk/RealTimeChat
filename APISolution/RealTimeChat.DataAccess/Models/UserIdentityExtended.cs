
using Microsoft.AspNetCore.Identity;

namespace RealTimeChat.DataAccess.Models
{
    public class UserIdentityExtended : IdentityUser
    {
        public ICollection<UserConversationConnector> Connectors { get; set; }
    }
}
