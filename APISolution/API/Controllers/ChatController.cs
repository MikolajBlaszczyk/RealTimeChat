using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace RealTimeChat.API.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
