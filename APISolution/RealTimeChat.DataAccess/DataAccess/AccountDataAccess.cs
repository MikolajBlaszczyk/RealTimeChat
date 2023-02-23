using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChat.DataAccess.IdentityContext;

namespace RealTimeChat.DataAccess.DataAccess
{
    public class AccountDataAccess
    {
        public ApplicationContext DbContext { get; }

        public AccountDataAccess(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }

        public string? GetUserGuid(string username)
        {
            return DbContext.Users.FirstOrDefault(user => user.UserName == username)?.Id;
        }


    }
}
