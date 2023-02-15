using Microsoft.AspNetCore.Identity;

namespace RealTimeChat.API.DataAccess.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<FriendsModel> Friends { get; set; }
    public ICollection<InvitationModel> Invitations { get; set; }
}