using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.AccountLogic.AccountManager
{
    public class ClaimsManager
    {

        public async Task<Claim> ClaimGuid(string? Guid)
        {
            //TODO: add message
            if (Guid is null)
                throw new ArgumentNullException();


            return new Claim("GUID", Guid);
        }
    }
}
