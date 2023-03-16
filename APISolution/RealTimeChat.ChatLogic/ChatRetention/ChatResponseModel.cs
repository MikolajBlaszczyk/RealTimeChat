namespace RealTimeChat.ChatLogic.ChatRetention
{
    public class ChatResponseModel
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public static ChatResponseModel CreateChatResponse(bool success, string message = "")
        {
            return new ChatResponseModel { Success = success, Message = message };
        }
    }
}
