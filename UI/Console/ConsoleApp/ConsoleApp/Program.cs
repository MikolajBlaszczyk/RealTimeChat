using System.Net;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text;
using Microsoft.AspNetCore.Http.Connections;

const string  cs = "https://localhost:7234/api/Account/Login/";


var handler = new HttpClientHandler()
{
    CookieContainer = new CookieContainer(),
    UseCookies = true
};

HttpClient client = new HttpClient(handler);

var json = "{" + $"\"Username\":\"ApiTest\",\"Password\":\"password123\"" + "}";
StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

var response = await client.PostAsync(cs,content);

response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> cookieValues);

HubConnection hub = new HubConnectionBuilder()
    .WithUrl("https://localhost:7234/chat", options =>
    {
        
        options.Cookies = handler.CookieContainer; 
        options.Transports = HttpTransportType.WebSockets;
        options.HttpMessageHandlerFactory = _ => handler;
    }).WithAutomaticReconnect().Build();
hub.On<string>("ReceiveMessage", s => Console.WriteLine(s));
hub.On<string, string, string>("Notification", (status, user, guid) => 
    Console.WriteLine($"Notification: users {user} status has changed to {status}, guid: {guid}"));
await hub.StartAsync();



string? input = null;
while ((input = Console.ReadLine()) != string.Empty)
{
    hub.InvokeAsync("SendOthers", input).Wait();
}


