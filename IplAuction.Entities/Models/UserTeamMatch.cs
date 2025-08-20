namespace IplAuction.Entities.Models;

public class UserTeamMatch
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PlayerId { get; set; }
    public int AuctionId { get; set; }
    public int MatchId { get; set; }
    public User User { get; set; } = null!;
    public Player Player { get; set; } = null!;
    public Auction Auction { get; set; } = null!;
    public Match Match { get; set; } = null!;
}
