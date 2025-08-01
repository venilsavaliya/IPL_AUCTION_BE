namespace IplAuction.Entities.ViewModels.Auction;

public class AuctionFilterParam : PaginationParams
{
    public string? Search { get; set; }
    public string? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public int? SeasonId { get; set; }
}
