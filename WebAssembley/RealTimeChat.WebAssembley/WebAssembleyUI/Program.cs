using HelperLibrary.Helpers.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebAssembleyUI;
using WebAssembleyUI.Helpers;
using WebAssembleyUI.Helpers.Login;
using WebAssembleyUI.Pages;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<IRequestHandler, LoginHelper>();
builder.Services.AddHttpClient();
builder.Services.AddLogging();

await builder.Build().RunAsync();
