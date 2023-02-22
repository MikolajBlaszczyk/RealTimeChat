using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.BusinessLogic.FriendsLogic.Enums;
using System.Security.Claims;
using RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;

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
    public async Task<IActionResult> AddFriend([FromBody] string FriendUsername)
    {
        Logger.GenerateRequestLog(FriendsRequest.Add);

        var result = await RequestHandler.AddFriend(UserId, FriendUsername);

        if (result.Result == FriendsResponseResult.ServerError)
            return BadRequest("INTERNAL ERROR");
        
        return Ok(result.Message);
    }

    [HttpGet]
    [Route("GetAllFriends")]
    public async Task<IActionResult> GetAllFriends()
    {
        Logger.GenerateRequestLog(FriendsRequest.Get);

        var result = await RequestHandler.GetAllFriends(UserId);
        
        return Ok(result.Message);
    }

    [HttpDelete]
    [Route("Remove")]
    public async Task<IActionResult> RemoveFriend([FromBody]string friendUsername)
    {
        Logger.GenerateRequestLog(FriendsRequest.Remove);

        var result = await RequestHandler.RemoveFriend(UserId, friendUsername);
        
        return Ok(result.Message);
    }

    [HttpGet]
    [Route("Invitations")]
    public async Task<IActionResult> GetInvitations()
    {
        Logger.GenerateRequestLog(FriendsRequest.GetInvitations);

        var result = await RequestHandler.GetAllInvitations(UserId);
        
        return Ok(result.Message);
    }

    [HttpPost]
    [Route("InvitationResponse")]
    public async Task<IActionResult> InvitationResponse([FromBody]string friendUsername, bool response)
    {
        Logger.GenerateRequestLog(FriendsRequest.RespondToInvitation);

        var result = await RequestHandler.InvitationResponse(UserId, friendUsername, response);
        
        
        return Ok(result.Message);
    }
}