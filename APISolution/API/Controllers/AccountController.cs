using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.Models;
using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.AccountLogic.Interfaces;

namespace RealTimeChat.API.Controllers;

[Route("api/Account/")]
[AllowAnonymous]
public class AccountController : Controller
{
    private ILogger Logger { get; }
    private readonly IUserAccountRequestHandler RequestHandler;

    public AccountController(ILogger<AccountController> logger,IUserAccountRequestHandler requestHandler)
    {
        Logger = logger;
        RequestHandler = requestHandler;
    }
    
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserModel body)
    {
        var response = await RequestHandler.HandleRegisterRequest(body);
        
        return GenerateHttpResponse(response.Result, response.Message);
    }


    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] UserModel body)
    {
        var response = await RequestHandler.HandleLoginRequest(body);
        
        return GenerateHttpResponse(response.Result, response.Message);
    }

   
    [HttpPost]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        var response = await RequestHandler.HandleLogoutRequest();
        
        return GenerateHttpResponse(response.Result, response.Message);
    }


    [HttpGet]
    [Route("Users")]
    public async Task<IActionResult> GetUsers()
    {
        throw new NotImplementedException();
    }

    private ObjectResult GenerateHttpResponse(ResponseIdentityResult res, string message) => res switch
    {
        ResponseIdentityResult.UserNotCreated or ResponseIdentityResult.WrongCredentials or ResponseIdentityResult.ValidationPasswordFailed => BadRequest(message),
        ResponseIdentityResult.ServerError or ResponseIdentityResult.LogoutFail => StatusCode(500, message),
        ResponseIdentityResult.Success => Ok(message),
        _ => NotFound(message)
    };


}