using IplAuction.Entities.Enums;
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

    public DbSet<Bid> Bids { get; set; }

    public DbSet<UserTeam> UserTeams { get; set; }

    public DbSet<Match> Matches { get; set; }

    public DbSet<ScoringRule> ScoringRules { get; set; }

    public DbSet<Notification> Notifications { get; set; }
    
    public DbSet<BallEvent> BallEvents { get; set; }

    public DbSet<InningState> InningStates { get; set; }

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

        modelBuilder.Entity<ScoringRule>().HasData(
            new ScoringRule { Id = 1, EventType = CricketEventType.Run, Points = 1 },
            new ScoringRule { Id = 2, EventType = CricketEventType.Four, Points = 1 },
            new ScoringRule { Id = 3, EventType = CricketEventType.Six, Points = 2 },
            new ScoringRule { Id = 4, EventType = CricketEventType.HalfCentury, Points = 4 },
            new ScoringRule { Id = 5, EventType = CricketEventType.Century, Points = 8 },
            new ScoringRule { Id = 6, EventType = CricketEventType.Duck, Points = -2 },
            new ScoringRule { Id = 7, EventType = CricketEventType.Catch, Points = 8 },
            new ScoringRule { Id = 8, EventType = CricketEventType.ThreeCatchHaul, Points = 4 },
            new ScoringRule { Id = 9, EventType = CricketEventType.Stumping, Points = 12 },
            new ScoringRule { Id = 10, EventType = CricketEventType.DirectRunOut, Points = 12 },
            new ScoringRule { Id = 11, EventType = CricketEventType.AssistedRunOut, Points = 6 },
            new ScoringRule { Id = 12, EventType = CricketEventType.Wicket, Points = 25 },
            new ScoringRule { Id = 13, EventType = CricketEventType.ThreeWicketHaul, Points = 4 },
            new ScoringRule { Id = 14, EventType = CricketEventType.FourWicketHaul, Points = 8 },
            new ScoringRule { Id = 15, EventType = CricketEventType.FiveWicketHaul, Points = 16 },
            new ScoringRule { Id = 16, EventType = CricketEventType.MaidenOver, Points = 12 }
        );

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

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(n => n.Notifications)
            .HasForeignKey(n => n.UserId);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.TeamA)
            .WithMany()
            .HasForeignKey(m => m.TeamAId);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.TeamB)
            .WithMany()
            .HasForeignKey(m => m.TeamBId);

        modelBuilder.Entity<ScoringRule>()
            .Property(sr => sr.EventType)
            .HasConversion<string>();
        
        modelBuilder.Entity<BallEvent>()
            .HasOne(b => b.Match)
            .WithMany(m => m.BallEvents)
            .HasForeignKey(b => b.MatchId);

        modelBuilder.Entity<BallEvent>()
            .HasOne(b => b.Batsman)
            .WithMany()
            .HasForeignKey(b => b.BatsmanId)
            .OnDelete(DeleteBehavior.Restrict); // to prevent cascading deletes

        modelBuilder.Entity<BallEvent>()
            .HasOne(b => b.NonStriker)
            .WithMany()
            .HasForeignKey(b => b.NonStrikerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BallEvent>()
            .HasOne(b => b.Bowler)
            .WithMany()
            .HasForeignKey(b => b.BowlerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BallEvent>()
            .HasOne(b => b.DismissedPlayer)
            .WithMany()
            .HasForeignKey(b => b.DismissedPlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BallEvent>()
            .HasOne(b => b.Fielder)
            .WithMany()
            .HasForeignKey(b => b.FielderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BallEvent>()
            .Property(b => b.WicketType)
            .HasConversion<string>();

        modelBuilder.Entity<BallEvent>()
            .Property(b => b.ExtraType)
            .HasConversion<string>();

        modelBuilder.Entity<InningState>()
            .HasOne(i => i.Match)
            .WithMany(m => m.InningStates)
            .HasForeignKey(i => i.MatchId);

        modelBuilder.Entity<InningState>()
            .HasOne(i => i.Striker)
            .WithMany()
            .HasForeignKey(i => i.StrikerId);

        modelBuilder.Entity<InningState>()
            .HasOne(i => i.NonStriker)
            .WithMany()
            .HasForeignKey(i => i.NonStrikerId);

        modelBuilder.Entity<InningState>()
            .HasOne(i => i.Bowler)
            .WithMany()
            .HasForeignKey(i => i.BowlerId);
        
        
        base.OnModelCreating(modelBuilder);
    }
}
