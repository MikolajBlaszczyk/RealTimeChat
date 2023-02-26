using System.Net;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using RealTimeChat;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp =>
{
    var handler = new HttpClientHandler() { Credentials = CredentialCache.DefaultCredentials };
    return handler;
});

builder.Services.AddScoped<HttpClient>(sp => new HttpClient(sp.GetRequiredService<HttpClientHandler>()));

var app = builder.Build();

await app.RunAsync();