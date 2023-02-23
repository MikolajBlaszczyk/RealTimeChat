using Microsoft.AspNetCore.Identity;

namespace RealTimeChat.DataAccess.Models;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<FriendsModel> Friends { get; set; }
    public virtual ICollection<InvitationModel> Invitations { get; set; }
 	public ICollection<UserConversationConnector> Connectors { get; set; }
    public virtual Session ThisSession { get; set; }
}
