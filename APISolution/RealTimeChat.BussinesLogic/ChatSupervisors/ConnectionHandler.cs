using Microsoft.AspNetCore.SignalR;
using RealTimeChat.DataAccess.Interfaces;

namespace RealTimeChat.BusinessLogic.ChatSupervisors
{
    public class UserConnectionHandler
    {

        public const string GuidClaim = "GUID";
        private IHubDataAccess DataAccess { get; }

        public UserConnectionHandler(IHubDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }

        public async Task HandleUserConnection(HubCallerContext context)
        {
            string? guid = context.User.FindFirst(GuidClaim)?.Value;
            string? connectionId = context.ConnectionId;

            await DataAccess.UpdateSessionConnection(guid, connectionId);
        }

        public async Task HandleUserDisconnection(HubCallerContext context)
        {
            string? guid = context.User.FindFirst(GuidClaim)?.Value;

            await DataAccess.DeleteSessionConnection(guid);

        }
    }
}
