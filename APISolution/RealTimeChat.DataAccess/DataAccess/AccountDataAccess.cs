using RealTimeChat.DataAccess.Interfaces;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.DataAccess
{
    public class AccountDataAccess : IAccountDataAccess
    {
        private readonly IUserUtils UserDataAcces;

        public AccountDataAccess(IUserUtils userDataAcces)
        {
            UserDataAcces = userDataAcces;
        }

        public async Task<string?> GetUserGuid(string username)
        {
            ApplicationUser? user = await UserDataAcces.GetUserByUserName(username);

            if(user is null)
                throw new Exception();

            return user.Id;
        }


    }
}
