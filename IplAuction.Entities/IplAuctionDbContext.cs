using IplAuction.Entities.Helper;
using IplAuction.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace IplAuction.Entities;

public class IplAuctionDbContext : DbContext
{
    public IplAuctionDbContext(DbContextOptions<IplAuctionDbContext> options)
            : base(options)
    {
    }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Player> Players { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Team> Teams { get; set; }

    public DbSet<Auction> Auctions { get; set; }

    public DbSet<AuctionParticipants> AuctionParticipants { get; set; }

    public DbSet<AuctionPlayer> AuctionPlayers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User()
        {
            Id = 1,
            FirstName = "Admin",
            Email = "admin@tatvasoft.com",
            DateOfBirth = new DateOnly(1991, 12, 12),
            Gender = Enums.UserGender.Male,
            Role = Enums.UserRole.Admin,
            MobileNumber = "1234567890",
            PasswordHash = Password.HashPassword("admin")
        });

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .Property(u => u.Gender)
            .HasConversion<string>();

        modelBuilder.Entity<Auction>()
            .HasOne(a => a.Manager)
            .WithMany(u => u.ManagedAuction)
            .HasForeignKey(a => a.ManagerId);

        modelBuilder.Entity<Auction>()
            .Property(u => u.AuctionStatus)
            .HasConversion<string>();

        modelBuilder.Entity<AuctionParticipants>()
            .HasOne(ap => ap.User)
            .WithMany(u => u.AuctionParticipants)
            .HasForeignKey(ap => ap.UserId);

        modelBuilder.Entity<AuctionParticipants>()
            .HasOne(ap => ap.Auction)
            .WithMany(a => a.AuctionParticipants)
            .HasForeignKey(ap => ap.AuctionId);

        modelBuilder.Entity<Bid>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bids)
            .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Auction)
            .WithMany(a => a.Bids)
            .HasForeignKey(b => b.AuctionId);

        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Player)
            .WithMany(p => p.Bids)
            .HasForeignKey(b => b.PlayerId);

        modelBuilder.Entity<UserTeam>()
            .HasOne(ut => ut.User)
            .WithMany(u => u.UserTeams)
            .HasForeignKey(ut => ut.UserId);

        modelBuilder.Entity<UserTeam>()
            .HasOne(ut => ut.Auction)
            .WithMany(a => a.UserTeams)
            .HasForeignKey(ut => ut.AuctionId);

        modelBuilder.Entity<UserTeam>()
            .HasOne(ut => ut.Player)
            .WithMany(p => p.UserTeams)
            .HasForeignKey(ut => ut.PlayerId);

        modelBuilder.Entity<AuctionPlayer>()
            .HasOne(ap => ap.User)
            .WithMany(u => u.AuctionPlayers)
            .HasForeignKey(ap => ap.CurrentBidUserId);

        modelBuilder.Entity<AuctionPlayer>()
            .HasOne(ap => ap.Auction)
            .WithMany(a => a.AuctionPlayers)
            .HasForeignKey(ap => ap.AuctionId);

        modelBuilder.Entity<AuctionPlayer>()
            .HasOne(ap => ap.Player)
            .WithMany(p => p.AuctionPlayers)
            .HasForeignKey(ap => ap.PlayerId);

        modelBuilder.Entity<Player>()
            .Property(p => p.Skill)
            .HasConversion<string>();
       
        base.OnModelCreating(modelBuilder);
    }
}
