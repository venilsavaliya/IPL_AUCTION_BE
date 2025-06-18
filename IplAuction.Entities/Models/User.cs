using IplAuction.Entities.Enums;

namespace IplAuction.Entities.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = null;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

    public ICollection<Auction> ManagedAuction { get; set; } = [];

    public ICollection<AuctionParticipants> AuctionParticipants { get; set; } = [];

    public ICollection<Bid> Bids { get; set; } = [];

    public ICollection<UserTeam> UserTeams { get; set; } = [];

    public ICollection<AuctionPlayer> AuctionPlayers { get; set; } = [];
}
