using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.DataAccess.DataAccess;
using RealTimeChat.DataAccess.Interfaces;

namespace RealTimeChat.API.Controllers
{
    [Route("api/User/")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public readonly IApiDataAccess ApiDataAccess;

        public UserController(IApiDataAccess apiDataAccess)
        {
            ApiDataAccess = apiDataAccess;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ApiDataAccess.GetAllUsers());
        }
    }
}
