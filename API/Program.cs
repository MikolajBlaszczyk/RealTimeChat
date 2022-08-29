using API.Hubs;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.API.Helpers.Validators;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppContextConnection") ?? throw new InvalidOperationException("Connection string 'AppContextConnection' not found.");

builder.Services.AddDbContext<API.Data.ApplicationContext>(options =>
    options.UseSqlServer(connectionString, builder =>
    {
        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    }));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<API.Data.ApplicationContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});


builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
});


builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddResponseCompression(options =>
{
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Here register services
builder.Services.AddTransient<IUserValidator,UserValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();;
app.UseAuthorization();
app.UseCookiePolicy();


app.MapControllers();
app.MapHub<WebChatHub>("/chat");

app.Run();
