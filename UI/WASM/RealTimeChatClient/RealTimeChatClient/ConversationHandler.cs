namespace RealTimeChatClient
{
    internal class ConversationHandler
    {
        internal static string AttachToConversation(string username, string message)
        {
            return $"{username}: {message}\n";
        }
    }
}
