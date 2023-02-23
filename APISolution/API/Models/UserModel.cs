using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.DataAccess.Models;
using RealTimeChat.AccountLogic.Interfaces;

namespace RealTimeChat.API.Models;

public class UserModel : IUserModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    [Compare("Password")]
    public string? ConfirmPassword { get; set; }

    public string? Email { get; set; }

    public ApplicationUser ConvertToIdentityUser()
    {
        return new ApplicationUser() { UserName = Username, Email = Email };
    }
}