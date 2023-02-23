using System.Security.Claims;
using RealTimeChat.AccountLogic.Models;
using RealTimeChat.AccountLogic.Enums;

namespace RealTimeChat.AccountLogic.Interfaces;

public interface ILoginManager
{
    Task<ResponseModel> LoginUserAsync(IUserModel users, CancellationToken token);
    Task<ResponseModel> SignInAsync(IUserModel user, CancellationToken token, Claim claim);
    Task SignOutAsync(CancellationToken token);
}