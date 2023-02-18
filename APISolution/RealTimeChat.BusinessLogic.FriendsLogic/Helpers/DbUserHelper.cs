using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.DataAccess.Models;

namespace RealTimeChat.BusinessLogic.FriendsLogic.Helpers;

internal class DbUserHelper
{
    private ApplicationContext DbContext { get; }

    internal DbUserHelper(ApplicationContext context)
    {
        DbContext = context;
    }
    

}