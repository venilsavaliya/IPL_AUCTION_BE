namespace IplAuction.Entities.ViewModels.Bid;

public class BidResponseModel
{
    public BidResponseModel() {}

    public BidResponseModel(Models.Bid bid)
    {
        AuctionId = bid.AuctionId;
        UserId = bid.UserId;
        PlayerId = bid.PlayerId;
        Amount = bid.Amount;
    }
    public int AuctionId { get; set; }

    public int UserId { get; set; }

    public int PlayerId { get; set; }

    public int Amount { get; set; }
}

