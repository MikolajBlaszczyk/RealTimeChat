using RealTimeChat.BusinessLogic.AccountLogic.Enums;
using RealTimeChat.BusinessLogic.AccountLogic.Models;

namespace RealTimeChat.BusinessLogic.AccountLogic.Interfaces;

public interface IRegisterManager
{
    Task<ResponseModel> RegisterUserAsync(IUserModel userToRegister);
}