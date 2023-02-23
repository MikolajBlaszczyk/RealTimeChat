using Microsoft.AspNetCore.Identity;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.API.DataAccess.Models;

{
    public virtual ICollection<FriendsModel> Friends { get; set; }
    public virtual ICollection<InvitationModel> Invitations { get; set; }
 	public ICollection<UserConversationConnector> Connectors { get; set; }
    public virtual Session ThisSession { get; set; }
}
<<<<<<< .mine


=======
    public virtual Session ThisSession { get; set; }
}
>>>>>>> .theirs
