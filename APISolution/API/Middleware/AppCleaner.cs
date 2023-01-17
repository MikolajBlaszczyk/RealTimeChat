using RealTimeChat.BusinessLogic;
using RealTimeChat.BusinessLogic.AccountLogic.SessionManager;
namespace RealTimeChat.API.Middleware;

public class AppCleaner : IDisposable
{
    private readonly ISessionHandler _session;
    private readonly ClosureHandler _bcHandler;

    public AppCleaner(ISessionHandler session,ClosureHandler bcHandler)
    {
        _session = session;
        _bcHandler = bcHandler;
    }

    public async Task CleanAppSession()
    {
        await _session.TerminateAllSessions();
    }

    public async Task CleanBusinessLogic()
    {
        await _bcHandler.PerformNecessaryDataAccessAction();
    }

    public void Dispose()
    {
        if (_session != null)
        {
            _session.Dispose();
        }
    }
}