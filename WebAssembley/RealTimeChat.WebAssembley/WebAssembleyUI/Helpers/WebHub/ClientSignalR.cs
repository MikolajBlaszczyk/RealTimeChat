using HelperLibrary.Helpers.Http;
using Microsoft.AspNetCore.SignalR.Client;

namespace WebAssembleyUI.Helpers.WebHub;

public class ClientSignalR : RequestHandler, IClientSignalR
{

    private readonly ILogger logger;
    private IConfiguration Configuration { get; set; }

    public ClientSignalR(IConfiguration configuration, ILogger<ClientSignalR> _logger)
    {
        Configuration = configuration;
        logger = _logger;
    }

    public async Task<HubConnection> StartConnection(HttpClient client)
    {
        HubConnection connection = BuildConnection();
        //SetUpSignalRLogic(connection);

        try
        {
            await connection.StartAsync();
            
            if(connection.ConnectionId != null)
                await UpdateConnectionIdForUser(client, connection.ConnectionId);

            logger.LogInformation($"Connection is started: {connection.ConnectionId}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }

        return connection;
    }

    private async Task<bool> UpdateConnectionIdForUser(HttpClient client, string connectionId)
    {

        var model = new ConnectionModel{ ConnectionId = connectionId };
        var response = await SendRequest(client, model.GetData(), Configuration.GetSection("URL")["UpdateInfo"]);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    private HubConnection BuildConnection()
    {
        var host = Configuration.GetSection("URL")["WebHub"];
        HubConnection connection = new HubConnectionBuilder()
            .WithUrl(host)
            .Build();

        return connection;
    }

}