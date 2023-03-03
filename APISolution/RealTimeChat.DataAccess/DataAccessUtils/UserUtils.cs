using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.DataAccessUtils
{
    public class UserUtils
    {
        private readonly ApplicationContext DataAccess;

        public UserUtils(ApplicationContext dataAccess)
        {
            DataAccess = dataAccess;
        }

        public async Task<ApplicationUser?> GetUserByUserName(string userName)
        {
            return DataAccess.Users.FirstOrDefault(x => x.UserName == userName);
        }

        public async Task<ApplicationUser?> GetUserByConnection(string connectionID)
        {
            return DataAccess.Users
                .Include(user => user.ThisSession)
                .FirstOrDefault(user => user.ThisSession.ConnectionID == connectionID);
        }

    }
}
