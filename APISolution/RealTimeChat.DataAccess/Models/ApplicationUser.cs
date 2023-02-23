using Microsoft.AspNetCore.Identity;

namespace RealTimeChat.DataAccess.Models;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<FriendsModel> Friends { get; set; }
    public virtual ICollection<InvitationModel> Invitations { get; set; }
}