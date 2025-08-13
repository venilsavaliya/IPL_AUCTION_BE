using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.AuctionPlayer;

public class AuctionPlayerFilterParams : PaginationParams
{
    public int AuctionId { get; set; }
    public string? Name { get; set; }
    public string? Skill { get; set; }
    public string? Status { get; set; }
}
