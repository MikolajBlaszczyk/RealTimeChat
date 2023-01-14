﻿using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.DataAccess.Models;
using RealTimeChat.BusinessLogic.AccountLogic.Interfaces;

namespace RealTimeChat.BusinessLogic.AccountLogic.SessionManager;

public class SessionHandler :ISessionHandler
{
    private readonly ApplicationContext DbContext;

    public SessionHandler(ApplicationContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task InitializeSession(IUserModel user)
    {
        Session sessionToInitialize = DataAccessModelFactory.CreateSessionModel(GetUserGuid(user.Username));

        DbContext.Session.Add(sessionToInitialize);
        await DbContext.SaveChangesAsync();
    }

    public async Task TerminateSession(string userName)
    {
        Session? sessionToTerminate = DbContext.Session.SingleOrDefault(session => session.UserGUID == GetUserGuid(userName));
            
        if (sessionToTerminate != null)
        {
            DbContext.Session.Remove(sessionToTerminate);
            await DbContext.SaveChangesAsync();
        }
    }

    public async Task TerminateAllSessions()
    {
        DbContext.Session.RemoveRange(DbContext.Session.ToList());
        await DbContext.SaveChangesAsync();
    }

    private string GetUserGuid(string userName)
    {
        var user = DbContext.Users.SingleOrDefault(user => user.UserName == userName);

        return user != null ? user.Id : null;
    }
    
    public void Dispose()
    {
        DbContext.Dispose();
    }
}