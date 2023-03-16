using Microsoft.AspNetCore.Http;
using RealTimeChat.AccountLogic.Enums;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.Models;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Models;

namespace RealTimeChat.AccountLogic.SessionManager;

public class SessionHandler :ISessionHandler
{
    private const string UserGUID = "GUID";

    private readonly IHttpContextAccessor ContextAccessor;
    private readonly ApplicationContext DbContext;

    public SessionHandler(ApplicationContext dbContext, IHttpContextAccessor contextAccessor)
    {
        ContextAccessor = contextAccessor;
        DbContext = dbContext;
    }

    public async Task<ResponseModel> InitializeSession()
    {
        
        Session sessionToInitialize = DataAccessModelFactory.CreateSessionModel(GetGuidClaims());

        try
        {
            DbContext.Session.Add(sessionToInitialize);
            await DbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return ResponseModel.CreateResponse(ResponseIdentityResult.ServerError);
        }

        return ResponseModel.CreateResponse(ResponseIdentityResult.Success);
    }

    public async Task<ResponseModel> TerminateSession()
    {
        Session? sessionToTerminate = DbContext.Session.SingleOrDefault(session => session.UserGUID == GetGuidClaims());

        try
        {
            DbContext.Session.Remove(sessionToTerminate);
            await DbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return ResponseModel.CreateResponse(ResponseIdentityResult.ServerError);
        }

        return ResponseModel.CreateResponse(ResponseIdentityResult.Success);
    }


    private string? GetGuidClaims()
    {
        return ContextAccessor.HttpContext.User.FindFirst(UserGUID)?.Value;
    }
}
