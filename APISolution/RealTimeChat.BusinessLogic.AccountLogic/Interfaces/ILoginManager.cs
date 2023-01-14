using Microsoft.AspNetCore.Identity;
using RealTimeChat.BusinessLogic.AccountLogic.Enums;
using RealTimeChat.BusinessLogic.AccountLogic.Models;

namespace RealTimeChat.BusinessLogic.AccountLogic.Interfaces;

public interface ILoginManager
{
    Task<ResponseModel> LoginUserAsync(IUserModel users);
    Task<ResponseModel> SignInAsync(IUserModel user);
    Task SignOutAsync();
}