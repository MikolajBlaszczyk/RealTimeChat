using Microsoft.AspNetCore.Identity;
using RealTimeChat.AccountLogic.Models;
using RealTimeChat.AccountLogic.Enums;

namespace RealTimeChat.AccountLogic.Interfaces;

public interface ILoginManager
{
    Task<ResponseModel> LoginUserAsync(IUserModel users);
    Task<ResponseModel> SignInAsync(IUserModel user);
    Task SignOutAsync();
}