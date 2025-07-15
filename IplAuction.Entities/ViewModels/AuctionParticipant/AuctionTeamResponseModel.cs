namespace IplAuction.Entities.ViewModels.AuctionParticipant;

public class AuctionTeamResponseModel
{
    public int AuctionId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public int BalanceLeft { get; set; }

    public int TotalPlayers { get; set; } 
    
    public string? ImageUrl { get; set; }
}
