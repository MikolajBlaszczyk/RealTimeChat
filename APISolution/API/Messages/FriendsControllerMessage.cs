namespace RealTimeChat.API.Messages;

public static class FriendsControllerMessage
{
    public const string AddRequest = "Add friend request";
    public const string AddSuccess = "Add friend success";
    public const string AddFail = "Add friend failed";
    
    public const string GetRequest = "Get all friends request";
    public const string GetSuccess = "Get all friends success";
    public const string GetFail = "Get all friends failed";
    
    public const string RemoveRequest = "Remove friend request";
    public const string RemoveSuccess = "Remove friend success";
    public const string RemoveFail = "Remove friend failed";
    
    public const string GetInvitationsRequest = "Get all invitations request";
    public const string GetInvitationsSuccess = "Get all invitations success";
    public const string GetInvitationsFail = "Get all invitations failed";
    
    public const string InvitationRespondRequest = "Invitation respond request";
    public const string InvitationRespondSuccess = "Invitation respond success";
    public const string InvitationRespondFail = "Invitation respond failed";
    
    public const string Error = "Something went wrong";
}