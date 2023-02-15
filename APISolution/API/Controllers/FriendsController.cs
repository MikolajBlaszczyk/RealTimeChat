using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.BusinessLogic.FriendsLogic.Enums;
using System.Security.Claims;

namespace RealTimeChat.API.Controllers;

[Route("api/Friends/")]
[Authorize]
public class FriendsController : Controller
{
    public FriendsCallLogger Logger { get; }

    private string Username => this.User.FindFirstValue(ClaimTypes.Name);

    public FriendsController(FriendsCallLogger logger)
    {
        Logger = logger;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddFriend(string FriendUsername)
    {
        Logger.GenerateRequestLog(FriendsRequest.Add);
        return Ok(Username);
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllFriends()
    {
        Logger.GenerateRequestLog(FriendsRequest.Get);
        
        return Ok("Get all");
    }

    [HttpDelete]
    [Route("Remove")]
    public async Task<IActionResult> RemoveFriend(string FriendUsername)
    {
        Logger.GenerateRequestLog(FriendsRequest.Remove);
        
        return Ok("Remove");
    }

    [HttpGet]
    [Route("Invitations")]
    public async Task<IActionResult> GetInvitations()
    {
        Logger.GenerateRequestLog(FriendsRequest.GetInvitations);
        
        return Ok("Requests");
    }

    [HttpPost]
    [Route("InvitationResponse")]
    public async Task<IActionResult> InvitationResponse(string FriendUsername, bool Response)
    {
        Logger.GenerateRequestLog(FriendsRequest.RespondToInvitation);
        
        return Ok("Invitation Response");
    }
}