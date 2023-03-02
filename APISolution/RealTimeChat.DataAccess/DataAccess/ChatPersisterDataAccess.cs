using RealTimeChat.DataAccess.IdentityContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChat.DataAccess.DataAccessUtils;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.DataAccess
{
    public class ChatPersisterDataAccess
    {
        public UserUtils UserContext { get; }
        public ConversationUtils ConversationUtils { get; }


        public ChatPersisterDataAccess(UserUtils userContext, ConversationUtils conversationUtils)
        {
            UserContext = userContext;
            ConversationUtils = conversationUtils;
        }

        public async Task<Conversation> GetConversation(string connectionID_A, string connectionID_B)
        {
            var user_A = UserContext.GetUserIDByConnection(connectionID_A);
            var user_B = UserContext.GetUserIDByConnection(connectionID_B);

            var conversation = await ConversationUtils.GetConversationByUsers(user_A, user_B);

            return conversation ?? new Conversation();
        }
    }
}
