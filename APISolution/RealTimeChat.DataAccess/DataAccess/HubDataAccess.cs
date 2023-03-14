using RealTimeChat.DataAccess.DataAccessUtils;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Interfaces;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.DataAccess
{
    public class HubDataAccess : IHubDataAccess
    {
        private readonly SessionUtils SessionUtils;

        public HubDataAccess(SessionUtils sessionUtils)
        {
            SessionUtils = sessionUtils;
        }


        public async Task UpdateSessionConnection(string guid, string connectionID)
        {
            Session sessionToUpdate = await SessionUtils.GetSessionByUserGuid(guid);

            sessionToUpdate.ConnectionID = connectionID;
            await SessionUtils.UpdateSession(sessionToUpdate); 
        }

        public async Task DeleteSessionConnection(string guid)
        {
            Session sessionToDelete = await SessionUtils.GetSessionByUserGuid(guid);

            await SessionUtils.DeleteSession(sessionToDelete);
        }
    }
}
