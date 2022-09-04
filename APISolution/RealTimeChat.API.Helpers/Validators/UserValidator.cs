using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.API.Helpers.Validators
{
    public class UserValidator : IUserValidator
    {
        public bool ValidatePassword(string password, string confirmPassword, out string message)
        {
            var result = true;
            message = "";
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

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;
            return true;
        }
    }
}
