using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.Auction;

public class AuctionResponseModel
{
    public AuctionResponseModel() { }
    public AuctionResponseModel(Models.Auction a)
    {
        Id = a.Id;
        ManagerId = a.ManagerId;
        StartDate = a.StartDate;
        AuctionStatus = a.AuctionStatus;
        Title = a.Title;
        MaximumPurseSize = a.MaximumPurseSize;
        MinimumBidIncreament = a.MinimumBidIncreament;
        ParticipantsUserIds = a.AuctionParticipants.Select(ap => ap.UserId).ToList();
        MaximumTeamsCanJoin = a.MaximumTeamsCanJoin;
        AuctionMode = a.ModeOfAuction;
    }

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int ManagerId { get; set; }

    public DateTime StartDate { get; set; }

    public int MaximumPurseSize { get; set; }

    public int MaximumTeamsCanJoin { get; set; }

    public int MinimumBidIncreament { get; set; }

    public List<int> ParticipantsUserIds { get; set; } = [];

    public AuctionStatus AuctionStatus { get; set; } = AuctionStatus.Scheduled;

    public bool AuctionMode { get; set; }
}
