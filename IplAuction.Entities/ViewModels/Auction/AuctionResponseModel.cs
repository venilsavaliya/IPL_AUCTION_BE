using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.Auction;

public class AuctionResponseModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int ManagerId { get; set; }

    public DateTime StartDate { get; set; }

    public int MaximumPurseSize { get; set; }

    public int MinimumBidIncreament { get; set; }

    public int TotalTeams { get; set; }

    public AuctionStatus AuctionStatus { get; set; } = AuctionStatus.Scheduled;
}
