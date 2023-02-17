namespace RealTimeChat.API.DataAccess.Models;

public class FriendsModel
{
    public string UserId { get; set; }
    public string FriendId { get; set; }
    
    public ApplicationUser User { get; set; }
    public ApplicationUser Friend { get; set; }
    
}