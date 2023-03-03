using Microsoft.Extensions.Logging;
using RealTimeChat.ChatLogic.ChatRetention;
using RealTimeChat.ChatLogic.Logic;
using RealTimeChat.DataAccess.DataAccess;

namespace RealTimeChat.ChatLogic
{
    public class ChatPersister
    {
        public ChatPersisterDataAccess DataAccess { get; }
        public MessageConverter Converter { get; }
        private ILogger<ChatPersister> Logger { get; }

        public ChatPersister(ChatPersisterDataAccess dataAccess, MessageConverter converter, ILogger<ChatPersister> logger)
        {
            DataAccess = dataAccess;
            Converter = converter;
            Logger = logger;
        }

        public async Task<ChatResponseModel> Save(string connectionID_A, string messages_A, string connectionID_B)
        {
            var conversation = await DataAccess.GetConversation(connectionID_A, connectionID_B);
            string? username = await DataAccess.GetUsername(connectionID_A);

            if (username is null)
                return ChatResponseModel.CreateChatResponse(false,"Username is invalid");

            if (string.IsNullOrEmpty(conversation.Message))
                conversation.Message = Converter.ConvertToJson(username, messages_A);
            else
                conversation.Message += Converter.ConvertToJson(username, messages_A);

            try
            {
                await DataAccess.SaveConversation(conversation);
            }
            catch (Exception e)
            {
                return ChatResponseModel.CreateChatResponse(false, "Failed saving conversation");
            }
            return ChatResponseModel.CreateChatResponse(true);
        }


    }
}
