namespace IplAuction.Entities.ViewModels.Match;

public class MatchFilterParams:PaginationParams
{
    public string? Search { get; set; }
    public string? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
