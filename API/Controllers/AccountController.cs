using API.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/Account/")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public IServiceProvider ServiceProvider { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public UserManager<AppUser> UserManager { get; }

        public AccountController(IServiceProvider serviceProvider, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            ServiceProvider = serviceProvider;
            SignInManager = signInManager;
            UserManager = userManager;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            var model = new AppUser() { UserName = Login };
            try
            {
                if (Password == ConfirmPassword)
                {
                    var result = await UserManager.CreateAsync(model, Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(model, isPersistent: false);
                        return Ok("Register succeeded");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Ex");
            }
            return BadRequest("error");
        }

        public async Task<IActionResult> Login(string Login, string Password, string ConfirmPassword)
        {
            var model = new AppUser() { UserName = Login };
            if (model.UserName != null && Password != null)
            {
                var result = await SignInManager.PasswordSignInAsync(model, Password, isPersistent: false,
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Ok("done");
                }
                else
                {
                    return BadRequest("error");
                }
            }
            else
            {
                return BadRequest("error");
            }
        }
    }
}
