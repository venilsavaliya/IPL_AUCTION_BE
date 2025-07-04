namespace IplAuction.Entities.ViewModels.AuctionPlayer;

public class AddAuctionPlayerRequest
{
     public int AuctionId { get; set; }

    public int PlayerId { get; set; }

    public bool IsSold { get; set; } = false;

    public bool IsAuctioned { get; set; } = false;

}
