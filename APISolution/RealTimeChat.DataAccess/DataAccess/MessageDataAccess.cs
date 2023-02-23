using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RealTimeChat.API.DataAccess.Models;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.KeyDataAccess
{
    //TODO: NULLABILITY CHECK
    public class MessageDataAccess
    {
        private readonly ApplicationContext DbContext;

        public MessageDataAccess(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }

        
        public IEnumerable<UserConversationConnector> GetAllUsersConversations(string UserGUID)
        {
            return DbContext.UsersConversation.Where(connector => connector.UserGUID == UserGUID);
        }
        
        public Conversation GetConversation(int conversationID)
        {
            return DbContext.Conversation.Find(conversationID);
        }

        public ApplicationUser GetUser(string guid)
        {
            return DbContext.Users.Find(guid);
        }
        

        public bool UpdateMessagesInConversation(int ConversationID, string message)
        {
            try
            {
                Conversation conversation = DbContext.Conversation.Find(ConversationID);

                conversation.Message += message;

                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool InsertMessage(string message, string firstUserGuid, string secondUserGuid)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    Conversation conversationToInsert = new Conversation
                    {
                        Message = message,
                        Part = 1
                    };

                    UserConversationConnector firstConversationConnector = new UserConversationConnector
                    {
                        Conversation = conversationToInsert,
                        User = GetUser(firstUserGuid)
                    };

                    UserConversationConnector secondConversationConnector = new UserConversationConnector
                    {
                        Conversation = conversationToInsert,
                        User = GetUser(secondUserGuid)
                    };

                    DbContext.Conversation.Add(conversationToInsert);
                    DbContext.UsersConversation.Add(firstConversationConnector);
                    DbContext.UsersConversation.Add(secondConversationConnector);

                    DbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }

            return true;
        }
        
    }
}
