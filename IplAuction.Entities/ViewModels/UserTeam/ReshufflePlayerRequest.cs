namespace IplAuction.Entities.ViewModels.UserTeam;

public class ReshufflePlayerRequest
{
    public int AuctionId { get; set; }
    public int PlayerId { get; set; }
    public int UserId { get; set; }
    public int PlayerBoughtPrice { get; set; }
}
