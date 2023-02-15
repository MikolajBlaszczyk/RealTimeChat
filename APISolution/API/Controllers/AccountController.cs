using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.Messages;
using RealTimeChat.API.Models;
using RealTimeChat.BusinessLogic.AccountLogic.Enums;
using RealTimeChat.BusinessLogic.AccountLogic.Interfaces;
using Serilog;
using Serilog.Core;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace RealTimeChat.API.Controllers;

[Route("api/Account/")]
[AllowAnonymous]
public class AccountController : Controller
{
    public AccountCallLogger Logger { get; }

    private readonly IUserAccountRequestHandler RequestHandler;
    
    public AccountController(AccountCallLogger logger,IUserAccountRequestHandler requestHandler)
    {
        Logger = logger;
        RequestHandler = requestHandler;

    }
    
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserModel body)
    {
        Logger.GenerateRequestLog(AccountRequest.Register);
        
        var response = await RequestHandler.HandleRegisterRequest(body);

        Logger.GenerateResponseLog(response.Result, AccountRequest.Register);
        
        return GenerateHttpResponse(response.Result, response.Message);
    }


    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] UserModel body)
    {
        Logger.GenerateRequestLog(AccountRequest.Login);
        
        var response = await RequestHandler.HandleLoginRequest(body);

        Logger.GenerateResponseLog(response.Result, AccountRequest.Login);

        return GenerateHttpResponse(response.Result, response.Message);
    }

   
    [HttpPost]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        Logger.GenerateRequestLog(AccountRequest.Logout);
        
        var response = await RequestHandler.HandleLogoutRequest();

        Logger.GenerateResponseLog(response.Result, AccountRequest.Logout);
        
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
