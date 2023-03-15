using RealTimeChat.AccountLogic.Models;

namespace RealTimeChat.AccountLogic.Interfaces;

public interface ILoginManager
{
    Task<ResponseModel> LoginUserAsync(IUserModel users, CancellationToken token);
    Task<ResponseModel> SignInAsync(IUserModel user, CancellationToken token);
    Task SignOutAsync(CancellationToken token);
}