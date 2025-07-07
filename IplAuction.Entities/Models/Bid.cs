namespace IplAuction.Entities.Models;

public class Bid
{
    public int Id { get; set; }

    public int AuctionId { get; set; }

    public int UserId { get; set; }

    public int PlayerId { get; set; }

    public int Amount { get; set; }

    public DateTime PlacedAt { get; set; }

    public Auction Auction { get; set; } = null!;

    public User User { get; set; } = null!;

    public Player Player { get; set; } = null!;
}

