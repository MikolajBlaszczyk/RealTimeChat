namespace RealTimeChat.BusinessLogic.AccountLogic.Validators;
public interface IAccountValidator
{
    bool IsModelValid(string password, string confirmPassword, string email, string username);
    bool IsPasswordValid(string password, string confirmPassword, ref string message);
    bool IsPasswordValid(string password);
}