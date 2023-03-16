using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.FriendsLogic.Enums;
using System.Security.Claims;
using RealTimeChat.FriendsLogic.Interfaces;

namespace RealTimeChat.API.Controllers;

[Route("api/Friends/")]
[Authorize]
public class FriendsController : Controller
{
    public FriendsCallLogger Logger { get; }
    public IFriendsRequestHandler RequestHandler { get; set; }

    private string UserId => this.User.FindFirstValue(ClaimTypes.NameIdentifier);

    public FriendsController(FriendsCallLogger logger, IFriendsRequestHandler requestHandler)
    {
        Logger = logger;
        RequestHandler = requestHandler;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddFriend([FromBody] string friendUsername)
    {
        Logger.GenerateRequestLog(FriendsRequest.Add);

        var response = await RequestHandler.AddFriend(UserId, friendUsername);

        Logger.GenerateResponseLog(response.Result, FriendsRequest.Add);
        
        return GenerateHttpResponse(response.Result, response.Message);
    }

    [HttpGet]
    [Route("GetAllFriends")]
    public async Task<IActionResult> GetAllFriends()
    {
        Logger.GenerateRequestLog(FriendsRequest.Get);

        var response = await RequestHandler.GetAllFriends(UserId);
        
        Logger.GenerateResponseLog(response.Result, FriendsRequest.Get);
        
        return GenerateHttpResponse(response.Result, response.Message);
    }

    [HttpDelete]
    [Route("Remove")]
    public async Task<IActionResult> RemoveFriend([FromBody]string friendUsername)
    {
        Logger.GenerateRequestLog(FriendsRequest.Remove);

        var response = await RequestHandler.RemoveFriend(UserId, friendUsername);
        
        Logger.GenerateResponseLog(response.Result, FriendsRequest.Remove);
        
        return GenerateHttpResponse(response.Result, response.Message);
    }

    [HttpGet]
    [Route("Invitations")]
    public async Task<IActionResult> GetInvitations()
    {
        Logger.GenerateRequestLog(FriendsRequest.GetInvitations);

        var response = await RequestHandler.GetAllInvitations(UserId);
        
        Logger.GenerateResponseLog(response.Result, FriendsRequest.GetInvitations);
        
        return GenerateHttpResponse(response.Result, response.Message);
    }

    [HttpPost]
    [Route("InvitationResponse")]
    public async Task<IActionResult> InvitationResponse([FromBody]string friendUsername, bool response)
    {
        Logger.GenerateRequestLog(FriendsRequest.RespondToInvitation);

        var requestResponse = await RequestHandler.InvitationResponse(UserId, friendUsername, response);
        
        Logger.GenerateResponseLog(requestResponse.Result, FriendsRequest.RespondToInvitation);
        
        return GenerateHttpResponse(requestResponse.Result, requestResponse.Message);
    }

    private ObjectResult GenerateHttpResponse(FriendsResponseResult result, string message) => result switch
    {
        FriendsResponseResult.Fail or FriendsResponseResult.AlreadyFriend or FriendsResponseResult.InvalidUser => BadRequest(message),
        FriendsResponseResult.ServerError => StatusCode(500, message),
        FriendsResponseResult.Success => Ok(message),
        _ => NotFound(message)
    };
}