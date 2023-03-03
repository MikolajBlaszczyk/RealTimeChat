using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.DataAccessUtils
{
    public class SessionUtils
    {
        private readonly ApplicationContext DbContext;
        public SessionUtils(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Session> GetSessionByUserGuid(string userGuid)
        {
            Session? session = DbContext.Session.FirstOrDefault(session => session.UserGUID == userGuid);

            if (session is null)
                throw new Exception();

            return session;
        }

        public async Task UpdateSession(Session session)
        {
            DbContext.Session.Update(session);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteSession(Session session)
        {
            DbContext.Session.Remove(session);
            await DbContext.SaveChangesAsync();
        }
    }
}
