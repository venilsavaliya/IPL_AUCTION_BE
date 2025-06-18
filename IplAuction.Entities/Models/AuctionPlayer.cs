namespace IplAuction.Entities.Models;

public class AuctionPlayer
{
    public int Id { get; set; }

    public int AuctionId { get; set; }

    public int PlayerId { get; set; }

    public bool IsSold { get; set; } = false;

    public bool IsAuctioned { get; set; } = false;

    public int? CurrentBidUserId { get; set; }

    public Auction Auction { get; set; } = null!;

    public Player Player { get; set; } = null!;

    public User User { get; set; } = null!;
}
