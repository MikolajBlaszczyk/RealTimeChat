namespace RealTimeChat.API.Helpers.Validators;

public interface IUserValidator
{
    bool ValidatePassword(string password, string confirmPassword, out string message);
    bool ValidatePassword(string password);
}