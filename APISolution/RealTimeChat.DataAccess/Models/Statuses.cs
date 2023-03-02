using System.ComponentModel.DataAnnotations;

namespace RealTimeChat.DataAccess.Models;

public class Statuses
{
    [Key]
    public int StatusId { get; set; }
    public string StatusName { get; set; }
}