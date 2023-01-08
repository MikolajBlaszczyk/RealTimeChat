using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.Models;
using RealTimeChat.BusinessLogic.AccountLogic.Enums;
using RealTimeChat.BusinessLogic.AccountLogic.Interfaces;

namespace RealTimeChat.API.Controllers;

[Route("api/Account/")]
[AllowAnonymous]
public class AccountController : Controller
{
    private readonly IUserAccountRequestHandler RequestHandler;

    public AccountController(IUserAccountRequestHandler requestHandler)
    {
        RequestHandler = requestHandler;
    }
    //TODO: create ResponseResult containing message, response, success
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserModel body)
    {
        RequestHandler.HandleRegisterRequest(body,out string message, out ResponseIdentityResult response);

        return GenerateResponse(response, message);
    }


    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] UserModel body)
    {
        RequestHandler.HandleLoginRequest(body, out string message, out ResponseIdentityResult response);

        return GenerateResponse(response, message);
    }

   
    [HttpPost]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        RequestHandler.HandleLogoutRequest(out string message, out ResponseIdentityResult response);
        

        return GenerateResponse(response, message);
    }


    [HttpGet]
    [Route("Users")]
    public async Task<IActionResult> GetUsers()
    {

        return Ok();
    }

    private ObjectResult GenerateResponse(ResponseIdentityResult res, string message) => res switch
    {
        ResponseIdentityResult.UserNotCreated or ResponseIdentityResult.WrongCredentials or ResponseIdentityResult.ValidationPasswordFailed => BadRequest(message),
        ResponseIdentityResult.ServerError or ResponseIdentityResult.LogoutFail => StatusCode(500, message),
        ResponseIdentityResult.Success => Ok(message),
        _ => NotFound(message)
    };
}