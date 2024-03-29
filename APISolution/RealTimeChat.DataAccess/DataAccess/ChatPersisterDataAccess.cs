﻿using RealTimeChat.DataAccess.Models;
using RealTimeChat.DataAccess.Interfaces;

namespace RealTimeChat.DataAccess.DataAccess
{
    public class ChatPersisterDataAccess : IChatPersisterDataAccess
    {
        private readonly IUserUtils UserContext;
        private readonly IConversationUtils ConversationUtils;


        public ChatPersisterDataAccess(IUserUtils userContext, IConversationUtils conversationUtils)
        {
            UserContext = userContext;
            ConversationUtils = conversationUtils;
        }

        public async Task<Conversation> GetConversation(string connectionID_A, string connectionID_B)
        {
            var user_A = await UserContext.GetUserByConnection(connectionID_A);
            var user_B = await UserContext.GetUserByConnection(connectionID_B);

            if (user_A is null || user_B is null)
                throw new Exception();

            var conversation = await ConversationUtils.GetConversationByUsers(user_A.Id, user_B.Id);

            return conversation ?? new Conversation();
        }

        public async Task<string> GetUsername(string connectionID)
        {
            var user = await UserContext.GetUserByConnection(connectionID);

            if(user is null)
                throw new Exception();

            return user.UserName;
        }

        public async Task SaveConversation(Conversation conversation) => await ConversationUtils.SaveConversation(conversation);
        
    }
}
