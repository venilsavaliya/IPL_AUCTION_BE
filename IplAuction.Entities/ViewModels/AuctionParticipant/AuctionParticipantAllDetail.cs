namespace IplAuction.Entities.ViewModels.AuctionParticipant;

public class AuctionParticipantAllDetail : AuctionParticipantDetail
{
    public int TotalParticipants { get; set; }

    public int BalanceLeft { get; set; }

    public int BestScore { get; set; }
}
