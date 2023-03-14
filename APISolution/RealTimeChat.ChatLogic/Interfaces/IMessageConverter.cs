namespace RealTimeChat.ChatLogic.Interfaces;

public interface IMessageConverter
{
    string ConvertToJson(string username, string message);
}