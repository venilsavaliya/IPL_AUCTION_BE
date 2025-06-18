using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.Auction;

public class AuctionFilterModel
{
    public PaginationParams Pagination { get; set; } = new();
    public string? Search { get; set; }
    public AuctionStatus? Status = null;
    public DateTime? StartDate = null;
    public DateTime? EndDate = null;
}
