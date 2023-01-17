using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.BusinessLogic.AccountLogic.Interfaces;

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

    public IdentityUser ConvertToIdentityUser()
    {
        return new IdentityUser() { UserName = Username, Email = Email };
    }
}