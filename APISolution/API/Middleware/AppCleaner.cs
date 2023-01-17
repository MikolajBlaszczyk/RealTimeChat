using RealTimeChat.BusinessLogic.AccountLogic.SessionManager;
namespace RealTimeChat.API.Middleware;

public class AppCleaner : IDisposable
{
    private readonly ISessionHandler _session;

    public AppCleaner(ISessionHandler session)
    {
        _session = session;
    }

    public async Task CleanAppSession()
    {
        await _session.TerminateAllSessions();
    }

    public void Dispose()
    {
        if (_session != null)
        {
            _session.Dispose();
        }
    }
}