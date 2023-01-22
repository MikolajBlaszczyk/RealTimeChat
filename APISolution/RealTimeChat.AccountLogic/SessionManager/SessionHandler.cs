using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RealTimeChat.AccountLogic.Interfaces;
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

    public async Task InitializeSession()
    {
        Session sessionToInitialize = DataAccessModelFactory.CreateSessionModel(GetGUIDClaims());

        DbContext.Session.Add(sessionToInitialize);
        await DbContext.SaveChangesAsync();
    }

    public async Task TerminateSession()
    {
        Session? sessionToTerminate = DbContext.Session.SingleOrDefault(session => session.UserGUID == GetGUIDClaims());
            
        if (sessionToTerminate != null)
        {
            DbContext.Session.Remove(sessionToTerminate);
            await DbContext.SaveChangesAsync();
        }
    }


    public string GetGUIDClaims()
    {
        return ContextAccessor.HttpContext.User.FindFirst(UserGUID).Value;
    }
}