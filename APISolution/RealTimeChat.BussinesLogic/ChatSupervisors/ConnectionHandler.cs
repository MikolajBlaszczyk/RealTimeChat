using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RealTimeChat.DataAccess.DataAccess;

namespace RealTimeChat.BusinessLogic.WebSupervisors
{
    public class UserConnectionHandler
    {

        public const string GuidClaim = "GUID";
        private HubDataAccess DataAccess { get; }

        public UserConnectionHandler( HubDataAccess dataAccess)
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
