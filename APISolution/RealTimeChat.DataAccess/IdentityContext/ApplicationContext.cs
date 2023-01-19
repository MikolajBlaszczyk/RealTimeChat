using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.API.DataAccess.Models;

namespace RealTimeChat.API.DataAccess.IdentityContext;

public class ApplicationContext : IdentityDbContext<IdentityUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<Session> Session { get; set; }
}
