using System.Security.Cryptography.X509Certificates;
using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.AuctionParticipant;

public class UserAuctionResponseModel
{
    public UserAuctionResponseModel() {}

    public UserAuctionResponseModel(UserAuctionResponseModel u)
    {
        AuctionId = u.AuctionId;
        UserId = u.UserId;
        AuctionTitle = u.AuctionTitle;
        AuctionStatus = u.AuctionStatus;
        StartTime = u.StartTime;
        AmountRemaining = u.AmountRemaining;
        TotalPlayer = u.TotalPlayer;
    }

    public int AuctionId { get; set; }

    public int UserId { get; set; }

    public string AuctionTitle { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public int TotalPlayer { get; set; }

    public int AmountRemaining { get; set; }

    public AuctionStatus AuctionStatus { get; set; }
}
