using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess.IdentityContext;

namespace RealTimeChat.DataAccess.KeyDataAccess
{
    public class MessageContext
    {
        private readonly ApplicationContext DbContext;

        public MessageContext(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }

        public bool SaveMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
