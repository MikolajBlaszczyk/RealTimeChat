
using System.Diagnostics;
using CliWrap;
using CliWrap.Buffered;

namespace RealTimeChat.API.Middleware;

public class AppCleaner : IDisposable
{
    private readonly ILogger<AppCleaner> _logger;

    public AppCleaner(ILogger<AppCleaner> logger)
    {
        _logger = logger;
    }
    public async Task CleanApp()
    {
        try
        {
            var result = await Cli.Wrap("python")
                .WithArguments(@"..\..\API.Cleaner\ApiCleaner.py")
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error,ex.ToString());   
        }
        
        
    }

    public void Dispose()
    {
        Process.GetCurrentProcess().Kill();
    }
}