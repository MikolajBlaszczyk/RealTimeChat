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
        private HubCallerContext Context { get; }
        private HubDataAccess DataAccess { get; }

        public UserConnectionHandler(HubCallerContext context, HubDataAccess dataAccess)
        {
            Context = context;
            DataAccess = dataAccess;
        }

        public async Task HandleUserConnection()
        {
            string? guid = Context.User.FindFirst(GuidClaim)?.Value;
            string? connectionId = Context.ConnectionId;

            await DataAccess.UpdateSessionConnection(guid, connectionId);
        }

        public async Task HandleUserDisconnection()
        {
            string? guid = Context.User.FindFirst(GuidClaim)?.Value;

            DataAccess.DeleteSessionConnection(guid);

        }
    }
}
