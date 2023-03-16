namespace RealTimeChat.DataAccess.Models;

public class Session
{
    public int SessionId { get; set; }
    public string UserGUID { get; set; }
    public DateTime SignInDate { get; set; }
    public string? ConnectionID { get; set; }
    
    public virtual ApplicationUser User { get; set; }
}