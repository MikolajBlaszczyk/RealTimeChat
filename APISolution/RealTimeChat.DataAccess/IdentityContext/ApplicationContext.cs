using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess.Models;
using System.Reflection.Emit;

namespace RealTimeChat.DataAccess.IdentityContext;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        
    }
    


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>()
            .HasOne(user => user.Status)
            .WithMany()
            .HasForeignKey(user => user.StatusId);
        

        builder.Entity<UserConversationConnector>()
            .HasKey(connector => new { connector.ConversationID, connector.UserGUID });


        builder.Entity<UserConversationConnector>()
            .HasOne(connector => connector.User)
            .WithMany(user => user.Connectors)
            .HasForeignKey(connector => connector.UserGUID);

        builder.Entity<UserConversationConnector>()
            .HasOne(connector => connector.Conversation)
            .WithMany(conversation => conversation.Connectors)
            .HasForeignKey(connector => connector.ConversationID);

        builder.Entity<Session>()
            .HasOne(session => session.User)
            .WithOne(user => user.ThisSession)
            .HasForeignKey<Session>(session => session.UserGUID);

        builder.Entity<FriendsModel>()
            .HasKey(f => new { f.UserId, f.FriendId });

        builder.Entity<FriendsModel>()
            .HasOne(f => f.User)
            .WithMany(u => u.Friends)
            .HasForeignKey(f => f.FriendId)
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

        builder.Entity<Statuses>()
            .HasData(new Statuses { StatusId = 1, StatusName = "Offline" },
                new Statuses { StatusId = 2, StatusName = "Online" });

        base.OnModelCreating(builder);
    }

    public DbSet<Session> Session { get; set; }
    public DbSet<Statuses> Statuses { get; set; }
    public DbSet<Conversation> Conversation { get; set; }
    public DbSet<UserConversationConnector> UsersConversation { get; set; }
  	public DbSet<InvitationModel> Invitations { get; set; }
    public DbSet<FriendsModel> Friends { get; set; }
}
