using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.AccountLogic.Enums;

public enum ResponseIdentityResult
{
    Success, 
    WrongCredentials,
    ValidationPasswordFailed,
    LogoutFail,
    UserNotCreated,
    ServerError
}