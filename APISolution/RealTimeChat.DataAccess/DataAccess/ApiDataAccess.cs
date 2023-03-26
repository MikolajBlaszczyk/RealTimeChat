using RealTimeChat.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.DataAccess.DataAccess
{
    public class ApiDataAccess : IApiDataAccess
    {
        public readonly IUserUtils UserUtils;

        public ApiDataAccess(IUserUtils userUtils)
        {
            UserUtils = userUtils;
        }

        public async Task<List<ApplicationUser>> GetAllUsers() =>  await UserUtils.GetAllUsers();
        
    }
}
