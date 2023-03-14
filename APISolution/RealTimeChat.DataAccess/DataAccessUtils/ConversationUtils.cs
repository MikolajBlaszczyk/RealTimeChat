using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Interfaces;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.DataAccessUtils
{
    public class ConversationUtils : IConversationUtils
    {

        private readonly ApplicationContext DbContext;

        public ConversationUtils(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }

        public async  Task<Conversation?> GetConversationByUsers(string userGuid_A, string userGuid_B)
        {
            var userConversations_A = DbContext.UsersConversation
                .Include(conversation => conversation.Conversation)
                .Where(user => user.UserGUID == userGuid_A);

            var userConversations_B = DbContext.UsersConversation
                .Include(conversation => conversation.Conversation)
                .Where(user => user.UserGUID == userGuid_B);

            UserConversationConnector? conversationBetweenUsers = userConversations_A.Intersect(userConversations_B).FirstOrDefault();

            if (conversationBetweenUsers != null)
                return conversationBetweenUsers.Conversation;
            else
                return null;
        }

        public async Task SaveConversation(Conversation conversation)
        {
            DbContext.Conversation.Add(conversation);
            await DbContext.SaveChangesAsync();
        }
    }
}
