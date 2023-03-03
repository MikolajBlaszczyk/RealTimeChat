using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.Models;
using RealTimeChat.DataAccess.DataAccess;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.AccountLogic.AccountManager
{

    /*
     *  At this point only GUID claim in necessary
     *  Required Claims for the RTC:
     *  -> GUID claim
     */
    public class ClaimsManager
    {
        private const string GuidClaim = "GUID";

        public SignInManager<ApplicationUser> SignInManager { get; }
        public AccountDataAccess DataAccess { get; }

        public ClaimsManager(SignInManager<ApplicationUser> signInManager, AccountDataAccess dataAccess)
        {
            SignInManager = signInManager;
            DataAccess = dataAccess;
        }


        public async Task<ResponseModel> RequireClaims(IUserModel model)
        {
            try
            {
                var claim = await CreateGuidClaim(model);
                await CreateGuidClaim(model, claim);
                await ClaimInContext(claim);
            }
            catch(Exception ex)
            {
                await SignInManager.SignOutAsync();
                return ResponseModel.CreateResponse(ResponseIdentityResult.ServerError);
            }
            
            return ResponseModel.CreateResponse(ResponseIdentityResult.Success);
        }

        public async Task<ResponseModel> SetContextClaim(IUserModel model)
        {
            try
            {
                var claim = await CreateGuidClaim(model);
                await ClaimInContext(claim);

            }
            catch (Exception ex)
            {
                await SignInManager.SignOutAsync();
                return ResponseModel.CreateResponse(ResponseIdentityResult.ServerError);
            }

            return ResponseModel.CreateResponse(ResponseIdentityResult.Success);
        }

        private async Task ClaimInContext(Claim claim)
        {
            SignInManager.Context.User.AddIdentity(new ClaimsIdentity(new[] { claim }));
        }

        private async Task CreateGuidClaim(IUserModel user, Claim claim)
        {
            var userToFind = await SignInManager.UserManager.FindByNameAsync(user.Username);
            await SignInManager.UserManager.AddClaimAsync(userToFind, claim);
            await SignInManager.RefreshSignInAsync(userToFind);
        }

        private async Task<Claim> CreateGuidClaim(IUserModel model)
        {
            string? guid = await DataAccess.GetUserGuid(model.Username);

            //TODO: add message
            if (model is null)
                throw new ArgumentNullException();

            return new Claim(GuidClaim, guid);
        }
    }
}
