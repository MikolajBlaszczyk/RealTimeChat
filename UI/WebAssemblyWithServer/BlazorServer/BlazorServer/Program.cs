using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.DataProtection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDataProtection().SetApplicationName("APP");

builder.Services.AddSingleton(sp =>
{
    var handler = new HttpClientHandler() {Credentials = CredentialCache.DefaultCredentials, CookieContainer = new CookieContainer(), UseCookies = true};
    // Modify the handler as needed
    return handler;
});

builder.Services.AddHttpClient("API").ConfigurePrimaryHttpMessageHandler(sp => sp.GetRequiredService<HttpClientHandler>());



builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddCors(policy =>
{
    policy.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(_ => true);
    });

    policy.AddPolicy("CORS",options =>
    {
        options.WithOrigins("https://localhost:7234", "http://localhost:5234")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("CORS");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
