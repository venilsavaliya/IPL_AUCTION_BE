using IplAuction.Entities.Enums;

namespace IplAuction.Entities.Models;

public class Auction
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int SeasonId { get; set; }
    public int ManagerId { get; set; }
    public DateTime StartDate { get; set; }
    public AuctionStatus AuctionStatus { get; set; } = AuctionStatus.Scheduled;
    public int MaximumTeamsCanJoin { get; set; } = 10;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int MinimumBidIncreament { get; set; } = 500000;
    public int MaximumPurseSize { get; set; } = 20000000;
    public int CurrentBid { get; set; } = 0;
    public bool ModeOfAuction { get; set; } = false;
    public int CurrentPlayerId { get; set; }
    public bool IsReshuffled { get; set; } = false;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public User Manager { get; set; } = null!;
    public ICollection<AuctionParticipants> AuctionParticipants { get; set; } = [];
    public ICollection<Bid> Bids { get; set; } = [];
    public Season? Season { get; set; }
    public ICollection<UserTeam> UserTeams { get; set; } = [];
    public ICollection<AuctionPlayer> AuctionPlayers { get; set; } = [];
    public ICollection<UserTeamMatch> UserTeamMatches { get; set; } = [];
}
