using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.API.Messages;
using RealTimeChat.API.Models;
using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.API.enums;
using Serilog;
using Serilog.Core;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace RealTimeChat.API.Controllers;

[Route("api/Account/")]
[AllowAnonymous]
public class AccountController : Controller
{
    private AccountCallLogger Logger { get; }
    private readonly IUserAccountRequestHandler RequestHandler;
    
    public AccountController(AccountCallLogger logger,IUserAccountRequestHandler requestHandler)
    {
        Logger = logger;
        RequestHandler = requestHandler;
    }
    
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserModel body, CancellationToken token)
    {
        Logger.GenerateRequestLog(AccountRequest.Register);
        
        var response = await RequestHandler.HandleRegisterRequest(body, token);

        Logger.GenerateResponseLog(response.Result, AccountRequest.Register);

        return GenerateHttpResponse(response.Result, response.Message);
    }


    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] UserModel body, CancellationToken token)
    {
        Logger.GenerateRequestLog(AccountRequest.Login);

        var response = await RequestHandler.HandleLoginRequest(body, token);

        Logger.GenerateResponseLog(response.Result, AccountRequest.Login);


        return GenerateHttpResponse(response.Result, response.Message);
    }

    [HttpPost]
    [Route("Logout")]
    public async Task<IActionResult> Logout(CancellationToken token)
    {
        Logger.GenerateRequestLog(AccountRequest.Logout);

        var response = await RequestHandler.HandleLogoutRequest(token);

        Logger.GenerateResponseLog(response.Result, AccountRequest.Logout);

        return GenerateHttpResponse(response.Result, response.Message);
    }

    [Authorize]
    [HttpGet]
    [Route("Users")]
    public async Task<IActionResult> GetUsers(CancellationToken token)
    {
        var cookie = HttpContext.Request.Cookies;

        return Ok();
    }

    [Authorize]
    [HttpGet]
    [Route("Token")]
    public async Task<IActionResult> GetToken(CancellationToken token)
    {
        var accessToken = Request.Cookies[CookieAuthenticationDefaults.AuthenticationScheme];

        if (accessToken == null)
        {
            return NotFound();
        }

        return Content(accessToken);
    }

    private ObjectResult GenerateHttpResponse(ResponseIdentityResult res, string message) => res switch
    {
        ResponseIdentityResult.UserNotCreated or ResponseIdentityResult.WrongCredentials or ResponseIdentityResult.ValidationPasswordFailed => BadRequest(message),
        ResponseIdentityResult.ServerError or ResponseIdentityResult.LogoutFail => StatusCode(500, message),
        ResponseIdentityResult.Success => Ok(message),
        _ => NotFound(message)
    };

}
