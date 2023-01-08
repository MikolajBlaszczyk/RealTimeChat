using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.DataAccess.Models;

namespace RealTimeChat.BusinessLogic.AccountLogic.SessionManager
{
    public class SessionHandler
    {
        private readonly ApplicationContext DbContext;

        public SessionHandler(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }


        public void InitializeSession(string userGuid)
        {
            Session sessionToInitialize = new Session();

            sessionToInitialize.UserGUID = userGuid;
            sessionToInitialize.SignInDate = DateTime.Now;

            DbContext.Session.Add(sessionToInitialize);
            DbContext.SaveChanges();
        }

        public void TerminateSession(string userGuid)
        {
            Session sessionToTerminate = DbContext.Session.Find(userGuid);
            
            DbContext.Remove(sessionToTerminate);
            DbContext.SaveChanges();
        }
    }
}
