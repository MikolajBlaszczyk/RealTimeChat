using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.API.DataAccess.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public string UserGUID { get; set; }
        public DateTime SignInDate { get; set; }
    }
}
