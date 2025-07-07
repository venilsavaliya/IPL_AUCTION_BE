namespace IplAuction.Entities.ViewModels.AuctionParticipant;

public class DeductBalanceRequest
{
    public int AuctionId { get; set; }

    public int UserId { get; set; }
    public int Amount { get; set; }
}
