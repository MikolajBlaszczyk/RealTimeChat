using System.Net;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text;
using Microsoft.AspNetCore.Http.Connections;

const string  cs = "https://localhost:7234/api/Account/Login/";
const string cs2 = "https://localhost:7234/api/Account/Users/";
const string cs3 = "https://localhost:7234/api/Account/Token/";

var handler = new HttpClientHandler()
{
    CookieContainer = new CookieContainer(),
    UseCookies = true
};

HttpClient client = new HttpClient(handler);

var json = "{" + $"\"Username\":\"ApiTest\",\"Password\":\"password123\"" + "}"; ;
StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

var response = await client.PostAsync(cs,content);

response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> cookieValues);

var response2 = await client.GetAsync(cs2);

HubConnection hub = new HubConnectionBuilder()
    .WithUrl("https://localhost:7234/chat", options =>
    {
        
        options.Cookies = handler.CookieContainer; 
        options.Transports = HttpTransportType.WebSockets;
        options.HttpMessageHandlerFactory = _ => handler;
    }).WithAutomaticReconnect().Build();

await hub.StartAsync();

Console.ReadLine();