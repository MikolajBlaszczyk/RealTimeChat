using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.API.Controllers;
using RealTimeChat.API.DataAccess.IdentityContext;
using RealTimeChat.API.DataAccess.Models;
using RealTimeChat.API.Middleware;
using RealTimeChat.BusinessLogic.AccountLogic;
using RealTimeChat.BusinessLogic.AccountLogic.AccountManager;
using RealTimeChat.BusinessLogic.AccountLogic.Interfaces;
using RealTimeChat.BusinessLogic.AccountLogic.SessionManager;
using RealTimeChat.BusinessLogic.AccountLogic.Validators;
using RealTimeChat.BusinessLogic.FriendsLogic;
using RealTimeChat.BusinessLogic.FriendsLogic.FriendsManagerDir;
using RealTimeChat.BusinessLogic.FriendsLogic.Helpers;
using RealTimeChat.BusinessLogic.FriendsLogic.Interfaces;
using RealTimeChat.BusinessLogic.FriendsLogic.InvitationsManagerDir;
using RealTimeChat.BusinessLogic.UserAvaliability;

namespace RealTimeChat.API.Startup;

public static class ProgramCleaner
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString)
    {
        //Entity
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                builder.MigrationsAssembly("RealTimeChat.API");
            }));
        //Identity 
        services.AddDefaultIdentity<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationContext>();
        
        services.Configure<IdentityOptions>(options =>
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
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        });
        //SignalR WebSocket. to chat
        services.AddSignalR();
        services.AddResponseCompression(options =>
        {
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
        });
        //Other
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });

        services.AddOptions<HostOptions>().Configure(options =>
        {
            options.ShutdownTimeout = TimeSpan.FromSeconds(20);
        });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        //Dependency injection
        services.AddTransient<AppCleaner,AppCleaner>();
        services.AddTransient<IRegisterManager, RegisterManager>();
        services.AddTransient<ILoginManager, LoginManager>();
        services.AddTransient<ISessionHandler, SessionHandler>();
        services.AddTransient<IUserAccountRequestHandler, UserAccountRequestHandler>();
        services.AddTransient<IAccountValidator, AccountValidator>();
        services.AddSingleton<IAvailablilityManager, AvailablilityManager>();
        services.AddTransient<AccountCallLogger, AccountCallLogger>();
        services.AddTransient<FriendsCallLogger, FriendsCallLogger>();
        services.AddTransient<IFriendsRequestHandler, FriendsRequestHandler>();
        services.AddTransient<IInvitationsManager, InvitationsManager>();
        services.AddTransient<IFriendsManager, FriendsManager>();
        services.AddTransient<IDbUserHelper, DbUserHelper>();

        return services;
    }
}