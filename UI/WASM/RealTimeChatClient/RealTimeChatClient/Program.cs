using System.Net;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RealTimeChatClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
{
    CookieContainer container = new CookieContainer();
    HttpClientHandler handler = new HttpClientHandler
    {
        CookieContainer = container
    };

    HttpHandler.CookieContainer = container;

    return new HttpClient(handler) { BaseAddress = new Uri("https://localhost:7234/") };
});
    

await builder.Build().RunAsync();
