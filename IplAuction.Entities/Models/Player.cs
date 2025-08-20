using IplAuction.Entities.Enums;

namespace IplAuction.Entities.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public PlayerSkill Skill { get; set; }
    public int TeamId { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public bool IsActive { get; set; } = true;
    public string Country { get; set; } = null!;
    public decimal BasePrice { get; set; }
    public string? Image { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = null;
    public ICollection<Bid> Bids { get; set; } = [];
    public Team Team { get; set; } = null!;
    public ICollection<UserTeam> UserTeams { get; set; } = [];
    public ICollection<AuctionPlayer> AuctionPlayers { get; set; } = [];
    public ICollection<PlayerMatchStates> PlayerMatchStates { get; set; } = [];
     public ICollection<UserTeamMatch> UserTeamMatches { get; set; } = [];
}
