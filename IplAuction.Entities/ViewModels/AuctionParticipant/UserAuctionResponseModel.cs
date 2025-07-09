using System.Security.Cryptography.X509Certificates;
using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.AuctionParticipant;

public class UserAuctionResponseModel
{
    public int AuctionId { get; set; }

    public int UserId { get; set; }

    public string AuctionTitle { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public int TotalPlayer { get; set; }

    public int TotalAmountSpent { get; set; }

    public AuctionStatus AuctionStatus { get; set; }
}
