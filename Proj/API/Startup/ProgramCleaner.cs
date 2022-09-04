using API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.API.Helpers.Validators;

namespace RealTimeChat.API.Startup
{
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
            services.AddDefaultIdentity<IdentityUser>()
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
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //Dependency injection
            services.AddTransient<IUserValidator, UserValidator>();

            return services;
        }
    }
}
