using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.BusinessLogic.AccountLogic.Validators;

public class AccountValidator : IAccountValidator
{
    public bool IsModelValid(string password, string confirmPassword, string email, string username)
    {
        throw new NotImplementedException();
    }

    public bool IsPasswordValid(string password, string confirmPassword, out string message)
    {
        var result = true;
        message = string.Empty;
        if (string.IsNullOrEmpty(password))
        {
            message = "password is empty";
            result = false;
        }
        if (string.IsNullOrEmpty(confirmPassword))
        {
            message = "confirm password is empty";
            result = false;
        }
        if (password.Length < 6)
        {
            message = "password is too short";
            result = false;
        }
        if (confirmPassword.Length < 6)
        {
            message = "confirm password is too short";
            result = false;
        }
        if (password != confirmPassword)
        {
            message = "passwords did not match";
            result = false;
        }
            
        return result;
    }

    public bool IsPasswordValid(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;
        return true;
    }
}