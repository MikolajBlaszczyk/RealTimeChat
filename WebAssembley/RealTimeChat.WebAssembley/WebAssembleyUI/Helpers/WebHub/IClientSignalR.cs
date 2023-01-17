using Microsoft.AspNetCore.SignalR.Client;

namespace WebAssembleyUI.Helpers.WebHub;

public interface IClientSignalR
{
    Task<HubConnection> StartConnection(HttpClient client);
}