namespace IplAuction.Entities.ViewModels.AuctionParticipant;

public class AuctionParticipantMantchDetail
{
    public int MatchId { get; set; }

    public string TeamName { get; set; } = null!;

    public DateTime Date { get; set; }

    public int UserPoints { get; set; }

    public int Rank { get; set; }

    public double Share { get; set; }
}
