namespace RealTimeChat.DataAccess.Models;

public class FriendsModel
{
    public string UserId { get; set; }
    public string FriendId { get; set; }
    
    public virtual ApplicationUser User { get; set; }
    public virtual ApplicationUser Friend { get; set; }
    
}