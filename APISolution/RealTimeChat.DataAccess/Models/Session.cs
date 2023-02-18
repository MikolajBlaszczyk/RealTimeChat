using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChat.API.DataAccess.Models;

namespace RealTimeChat.DataAccess.Models;

public class Session
{
    public int SessionId { get; set; }
    public string UserGUID { get; set; }
    public DateTime SignInDate { get; set; }
    
    public virtual ApplicationUser User { get; set; }
}