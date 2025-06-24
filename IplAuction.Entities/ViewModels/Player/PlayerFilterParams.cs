namespace IplAuction.Entities.ViewModels.Player;

public class PlayerFilterParams : PaginationParams
{
    public string? Skill { get; set; }
    public int? TeamId { get; set; }
    public string? Search { get; set; }
    public bool? ActiveStatus { get; set; }
}
