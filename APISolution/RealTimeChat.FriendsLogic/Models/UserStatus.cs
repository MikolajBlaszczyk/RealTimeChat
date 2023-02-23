using System.ComponentModel.DataAnnotations;

namespace RealTimeChat.FriendsLogic.Models;

public class UserStatus
{
    [Key]
    public int StatusId { get; set; }
    public string Status { get; set; }
}