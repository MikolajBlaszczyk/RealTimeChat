using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.API.DataAccess.Models;


namespace RealTimeChat.API.DataAccess.IdentityContext;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<FriendsModel>()
            .HasKey(f => new { f.UserId, f.FriendId });

        builder.Entity<FriendsModel>()
            .HasOne(f => f.User)
            .WithMany(u => u.Friends)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<FriendsModel>()
            .HasOne(f => f.Friend)
            .WithMany()
            .HasForeignKey(f => f.FriendId);

        
        
        builder.Entity<InvitationModel>()
            .HasKey(i => new { i.SenderId, i.ResponderId });

        builder.Entity<InvitationModel>()
            .HasOne(i => i.Sender)
            .WithMany(s => s.Invitations)
            .HasForeignKey(i => i.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<InvitationModel>()
            .HasOne(i => i.Responder)
            .WithMany()
            .HasForeignKey(i => i.ResponderId);

        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Session> Session { get; set; }
    public DbSet<InvitationModel> Invitations { get; set; }
    public DbSet<FriendsModel> Friends { get; set; }
    
}
