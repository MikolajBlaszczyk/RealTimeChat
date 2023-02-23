using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.DataAccess
{
    public class HubDataAccess
    {
        public ApplicationContext DbContext { get; }

        public HubDataAccess(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }


        public async Task UpdateSessionConnection(string guid, string connectionID)
        {
            Session? sessionToUpdate = DbContext.Session.FirstOrDefault(session => session.UserGUID == guid);

            if (sessionToUpdate is null)
                return;

            sessionToUpdate.ConnectionID = connectionID;
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteSessionConnection(string guid)
        {
            Session? sessionToDelete = DbContext.Session.FirstOrDefault(session => session.UserGUID == guid);

            if(sessionToDelete is null) 
                return;

            DbContext.Session.Remove(sessionToDelete);
            await DbContext.SaveChangesAsync();
        }
    }
}
