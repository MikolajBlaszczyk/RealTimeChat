using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess.IdentityContext;

namespace RealTimeChat.DataAccess.DataAccessUtils
{
    public class UserUtils
    {
        private readonly ApplicationContext DataAccess;

        public UserUtils(ApplicationContext dataAccess)
        {
            DataAccess = dataAccess;
        }

        public string GetUserIDByConnection(string connectionID)
        {
            var user = DataAccess.Users
                .Include(user => user.ThisSession)
                .FirstOrDefault(user => user.ThisSession.ConnectionID == connectionID);

            if (user != null)
                return user.Id;

            return string.Empty;
        }
    }
}
