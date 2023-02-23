using RealTimeChat.FriendsLogic.Enums;

namespace RealTimeChat.FriendsLogic.Models;


public class ResponseModel
{
    public FriendsResponseResult Result {get; set; }
    public string Message { get; set; }

    private ResponseModel(FriendsResponseResult result, string message)
    {
        Result = result;
        Message = message;
    }

    public static ResponseModel CreateResponse(FriendsResponseResult result, string message = "")
    {
        return new ResponseModel(result, message);
    }
}
