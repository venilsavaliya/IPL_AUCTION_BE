namespace IplAuction.Entities.ViewModels.AuctionParticipant;

public class AuctionParticipantRequestModel
{
    public int UserId { get; set; }

    public int AuctionId { get; set; }
}

public class AuctionParticipantResponseModel : AuctionParticipantRequestModel
{
    public string FullName { get; set; } = null!;

    public string Image { get; set; } = null!;

    public int PurseBalance { get; set; }
    
    public int TotalPlayer { get; set; }
}
