using RealTimeChat.BusinessLogic.FriendsLogic.Enums;

namespace RealTimeChat.API.DataAccess.Models;

public class InvitationModel
{
    public string SenderId { get; set; }
    public string ResponderId { get; set; }
    
    public ApplicationUser Sender { get; set; }
    public ApplicationUser Responder { get; set; }
    
    public string Status { get; set; } = String.Empty;
}