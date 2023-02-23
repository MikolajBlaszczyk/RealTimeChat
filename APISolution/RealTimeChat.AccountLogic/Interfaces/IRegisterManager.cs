using RealTimeChat.AccountLogic.Models;
using RealTimeChat.AccountLogic.Enums;

namespace RealTimeChat.AccountLogic.Interfaces;

public interface IRegisterManager
{
    Task<ResponseModel> RegisterUserAsync(IUserModel userToRegister);
}