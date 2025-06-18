using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.Auction;

public class AddAuctionRequestModel
{
    public string Title { get; set; } = null!;

    public int ManagerId { get; set; }

    public DateTime StartDate { get; set; }

    public int MinimumBid { get; set; }

    public int MaxPurseSize { get; set; }

    public AuctionStatus AuctionStatus { get; set; } = AuctionStatus.Scheduled;
}
