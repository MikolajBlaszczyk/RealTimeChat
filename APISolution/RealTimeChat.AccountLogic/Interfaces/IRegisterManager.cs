using RealTimeChat.AccountLogic.Models;

namespace RealTimeChat.AccountLogic.Interfaces;

public interface IRegisterManager
{
    Task<ResponseModel> RegisterUserAsync(IUserModel userToRegister, CancellationToken token);
}