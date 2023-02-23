using Microsoft.AspNetCore.Identity;
using RealTimeChat.API.DataAccess.Models;

namespace RealTimeChat.AccountLogic.Interfaces;

public interface IUserModel
{
    string Username { get; set; }
    string Email { get; set; }
    string Password { get; set; }
    string ConfirmPassword { get; set; }

    ApplicationUser ConvertToApplicationUser();
}
