using Microsoft.AspNetCore.Identity;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.API.DataAccess.Models;

public class ApplicationUser : IdentityUser
{

    public ICollection<UserConversationConnector> Connectors { get; set; }
    public virtual Session ThisSession { get; set; }
}