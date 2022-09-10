using API.Hubs;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.API.Helpers.Validators;
using RealTimeChat.API.Startup;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppContextConnection") ?? throw new InvalidOperationException("Connection string 'AppContextConnection' not found.");

builder.Services.RegisterServices(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.UseCookiePolicy();


app.MapControllers();
app.MapHub<WebChatHub>("/chat");

app.Run();
