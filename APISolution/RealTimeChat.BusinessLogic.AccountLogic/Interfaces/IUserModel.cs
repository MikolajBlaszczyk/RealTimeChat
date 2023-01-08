
using Microsoft.AspNetCore.Identity;

namespace RealTimeChat.BusinessLogic.AccountLogic.Interfaces;

public interface IUserModel
{
    string Username { get; set; }
    string Email { get; set; }
    string Password { get; set; }
    string ConfirmPassword { get; set; }

    IdentityUser ConvertToIdentityUser();
}