namespace IplAuction.Entities.ViewModels.AuctionPlayer;

public class AuctionPlayerRequest
{
    public int AuctionId { get; set; }

    public int PlayerId { get; set; }
}
public class AddAuctionPlayerRequest : AuctionPlayerRequest
{

    public bool IsSold { get; set; } = false;

    public bool IsAuctioned { get; set; } = false;

}
