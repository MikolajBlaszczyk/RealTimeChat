﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess.IdentityContext;
using RealTimeChat.AccountLogic;
using RealTimeChat.AccountLogic.AccountManager;
using RealTimeChat.AccountLogic.Interfaces;
using RealTimeChat.AccountLogic.SessionManager;
using RealTimeChat.AccountLogic.Validators;
using RealTimeChat.API.Controllers;
using RealTimeChat.API.LifeCycle;
using RealTimeChat.BusinessLogic.AvailabilityManager;
using RealTimeChat.BusinessLogic.ChatSupervisors;
using RealTimeChat.DataAccess.DataAccess;
using RealTimeChat.DataAccess.Models;
using RealTimeChat.FriendsLogic;
using RealTimeChat.FriendsLogic.FriendsManagers;
using RealTimeChat.FriendsLogic.Interfaces;
using RealTimeChat.ChatLogic;
using RealTimeChat.ChatLogic.Interfaces;
using RealTimeChat.DataAccess.DataAccessUtils;
using RealTimeChat.ChatLogic.Logic;
using RealTimeChat.DataAccess.Interfaces;

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
            }));
        //Identity 

        services.AddDefaultIdentity<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();


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

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });


        
        //SignalR WebSocket. to chat
        services.AddSignalR();
        services.AddResponseCompression(options =>
        {
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
        });
        //Other
        services.AddCors(options =>
        { ;

            options.AddPolicy("CORS", policy =>
            {
                policy.WithOrigins("https://localhost:7013")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();

                policy.WithOrigins("https://localhost:7272", "http://localhost:5277", "http://localhost:30420")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        services.AddOptions<HostOptions>().Configure(options =>
        {
            options.ShutdownTimeout = TimeSpan.FromSeconds(20);
        });

        services.AddMemoryCache();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        //Dependency injection
        services.AddHttpContextAccessor();
        services.AddTransient<UserConnectionHandler, UserConnectionHandler>();
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
        services.AddTransient<IFriendsDataAccess, FriendsDataAccess>();
        services.AddTransient<AccountCallLogger, AccountCallLogger>();
        services.AddTransient<IAccountDataAccess, AccountDataAccess>();
        services.AddTransient<IHubDataAccess, HubDataAccess>();
        services.AddTransient<ClaimsManager, ClaimsManager>();
        services.AddTransient<ISessionUtils,SessionUtils>();
        services.AddTransient<IUserUtils,UserUtils>();
        services.AddTransient<IConversationUtils, ConversationUtils>();
        services.AddTransient<IChatPersisterDataAccess, ChatPersisterDataAccess>();
        services.AddTransient<IChatPersister, ChatPersister>();
        services.AddTransient<IMessageConverter,MessageConverter>();
        services.AddTransient<IStatusDataAccess, StatusDataAccess>();
        services.AddTransient<StatusManager, StatusManager>();
        services.AddTransient<IApiDataAccess, ApiDataAccess>();

        return services;
    }
}