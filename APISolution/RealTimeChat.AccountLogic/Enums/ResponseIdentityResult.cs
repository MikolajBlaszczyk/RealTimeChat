namespace RealTimeChat.AccountLogic.Enums;

public enum ResponseIdentityResult
{
    Success, 
    WrongCredentials,
    ValidationPasswordFailed,
    LogoutFail,
    UserNotCreated,
    ServerError
}
