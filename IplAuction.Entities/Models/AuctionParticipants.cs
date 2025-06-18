namespace IplAuction.Entities.Models;

public class AuctionParticipants
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int AuctionId { get; set; }

    public int PurseBalance { get; set; } 

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;

    public Auction Auction { get; set; } = null!;
}
