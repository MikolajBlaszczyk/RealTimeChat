using System.Diagnostics;
using CliWrap;
using CliWrap.Buffered;

namespace RealTimeChat.API.LifeCycle;

public class AppCleaner : IDisposable
{
    private readonly ILogger<AppCleaner> _logger;
    private readonly IConfiguration _config;

    public AppCleaner(ILogger<AppCleaner> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }
    public async Task CleanApp()
    {
        try
        {
            var result = await Cli.Wrap("python")
                .WithArguments(@"..\..\API.Cleaner\ApiCleaner.py")
                .WithArguments(_config.GetConnectionString("AppContextConnection"))
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.ToString());
        }


    }

    public void Dispose()
    {
        Process.GetCurrentProcess().Kill();
    }
}