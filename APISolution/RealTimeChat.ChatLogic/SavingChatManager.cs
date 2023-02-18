using Microsoft.Extensions.Logging;
using RealTimeChat.ChatLogic.Models;
using RealTimeChat.DataAccess.KeyDataAccess;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.ChatLogic
{

    //TODO: NULLABILITY CHECK 
    public class SavingChatManager
    {
        private ILogger<SavingChatManager> Looger { get; }
        public MessageDataAccess DataAccess { get; }

        public SavingChatManager(ILogger<SavingChatManager> looger, MessageDataAccess dataAccess)
        {
            Looger = looger;
            DataAccess = dataAccess;
        }


        public ChatResponseModel Save(string messages, string firstUserGuid, string secondUserGuid)
        {
            if (ConversationExists(firstUserGuid, secondUserGuid))
            {
                var id = GetConversationIDBetweenUsers(firstUserGuid, secondUserGuid);

                DataAccess.UpdateMessagesInConversation(id, messages);
            }
            else
            {
                DataAccess.InsertMessage(messages, firstUserGuid, secondUserGuid);
            }

            return null;
        }

       
        public bool ConversationExists(string firstGuid, string secondGuid)
        {
            var conversation = GetUsersConversation(firstGuid, secondGuid);

            return (conversation is not null);
        }
        
        public int GetConversationIDBetweenUsers(string firstGuid, string secondGuid)
        {
            var connectorTable = GetUsersConversation(firstGuid, secondGuid);

            return connectorTable.ConversationID;
        }


        public UserConversationConnector? GetUsersConversation(string firstGuid, string secondGuid)
        {
            var firstUserConversations = DataAccess.GetAllUsersConversations(firstGuid);
            var secondUserConversations = DataAccess.GetAllUsersConversations(secondGuid);

            return firstUserConversations.FirstOrDefault(first =>
                secondUserConversations.Any(second => second.ConversationID == first.ConversationID));
        }
    }
}
