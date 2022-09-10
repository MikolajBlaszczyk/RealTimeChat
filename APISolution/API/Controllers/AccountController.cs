using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.API.Helpers.Validators;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace API.Controllers
{
    [Route("api/Account/")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public IServiceProvider ServiceProvider { get; }
        public SignInManager<IdentityUser> SignInManager { get; }
        public UserManager<IdentityUser> UserManager { get; }

        public AccountController(IServiceProvider serviceProvider, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            ServiceProvider = serviceProvider;
            SignInManager = signInManager;
            UserManager = userManager;
        }

        /// <summary>
        /// Api call that provides method to register a user.
        /// </summary>
        /// <param name="modelFromBody">{"Username": "value", "Password":"value", "ConfirmPassword":"value"}</param>
        /// <returns>Http result</returns>
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserModel body)
        {
            var validator = ServiceProvider.GetService<IUserValidator>();
            var model = new IdentityUser() { UserName= body.UserName, Email = body.Email};
            string message;

            if (validator.ValidatePassword(body.Password,body.ConfirmPassword, out message) == true)
            {
                IdentityResult result;
                try
                {
                    result = await UserManager.CreateAsync(model, body.Password);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(model, isPersistent: false);
                    return Ok("Register succeeded");
                }
             }

            return BadRequest(message);
        }


        /// <summary>
        /// Api call that provides method to login to the user account.
        /// </summary>
        /// <param name="modelFromBody">{"Username": "value", "Password":"value"}</param>
        /// <returns>Http result</returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserModel body)
        {
            var validator = ServiceProvider.GetService<IUserValidator>();
            if (validator.ValidatePassword(body.Password) == true)
            {
                var model = new IdentityUser() { UserName = body.UserName, Email = body.Email};
                SignInResult result;

                try
                {
                    result = await SignInManager.PasswordSignInAsync(model.UserName, body.Password, isPersistent: false,
                        lockoutOnFailure: true);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

                if (result.Succeeded)
                {
                    return Ok("Login completed");
                }
                else
                {
                    return BadRequest("User password does not match the login");
                }
            }
            else
            {
                return BadRequest("Password is null or is empty");
            }
        }

        /// <summary>
        /// Api call tha probides method to logout from the user account.
        /// </summary>
        /// <returns>Http result</returns>
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await SignInManager.SignOutAsync();
                return Ok("Signed out ");
            }
            catch (Exception ex)
            {
                return BadRequest("Something wen wrong");
            }
            
        }
    }
}
