using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RealTimeChat.API.Models;

namespace RealTimeChat.API.Controllers;

[Route("api/chat/")]
[AllowAnonymous]

public class ChatController : Controller
{
    private readonly IServiceProvider ServiceProvider;

    public ChatController(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
            
    }

    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> UpdateConnection([FromBody] ConnectionModel model)
    {
        var manager = ServiceProvider.GetService<ChatManager>();
        var claims = ClaimsPrincipal.Current.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault();    
        var outcome = await manager.UpdateConnectionInformation(model, "");

        if (outcome == true)
            return Ok("User Updated");
        else
            return BadRequest("User was not updated");

    }
}