using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.DataAccess.Models;
using RealTimeChat.API.DataAccess.KeyDataAccess;
using RealTimeChat.API.Middleware;
using RealTimeChat.AccountLogic;
using RealTimeChat.AccountLogic.AccountManager;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.SessionManager;
using RealTimeChat.AccountLogic.Validators;
using RealTimeChat.FriendsLogic;
using RealTimeChat.FriendsLogic.FriendsManagers;
using RealTimeChat.FriendsLogic.Helpers;
using RealTimeChat.FriendsLogic.Interfaces;

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
        services.AddTransient<DatabaseClosureManager, DatabaseClosureManager>();
        services.AddTransient<AppCleaner,AppCleaner>();
        services.AddTransient<IRegisterManager, RegisterManager>();
        services.AddTransient<ILoginManager, LoginManager>();
        services.AddTransient<ISessionHandler, SessionHandler>();
        services.AddTransient<IUserAccountRequestHandler, UserAccountRequestHandler>();
        services.AddTransient<IAccountValidator, AccountValidator>();
        services.AddTransient<FriendsCallLogger, FriendsCallLogger>();
        services.AddTransient<IFriendsRequestHandler, FriendsRequestHandler>();
        services.AddTransient<IInvitationsManager, InvitationsManager>();
        services.AddTransient<IFriendsManager, FriendsManager>();
        services.AddTransient<IDbUserHelper, DbUserHelper>();
        services.AddTransient<AccountCallLogger, AccountCallLogger>();

        return services;
    }
}