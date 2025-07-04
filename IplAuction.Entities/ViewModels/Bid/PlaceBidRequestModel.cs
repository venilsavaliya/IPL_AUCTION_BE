namespace IplAuction.Entities.ViewModels.Bid;

public class PlaceBidRequestModel
{
    public int AuctionId { get; set; }
    public int PlayerId { get; set; }
    public int BidAmount { get; set; }
    public int UserId { get; set; }
}
