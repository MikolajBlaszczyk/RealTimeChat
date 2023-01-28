namespace RealTimeChat.API.Messages;

public static class AccountControllerMessage
{
    public const string LoginRequest = "Login request";
    public const string LoginFailed = "Login failed";
    public const string LoginSuccess = "Login successfull";
    
    public const string RegisterRequest = "Register request";                     
    public const string RegisterFailed = "Registration failed";             
    public const string RegisterSuccess = "Registration successfull";

    public const string LogoutRequest = "Logout request";
    public const string LogoutFailed = "Logout failed";
    public const string LogoutSuccess = "Logout successfull";

    public const string Error = "Something went wrong";

}