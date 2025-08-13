using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.AuctionPlayer;

public class AuctionPlayerDetail
{
    public int PlayerId { get; set; }

    public string PlayerName { get; set; } = null!;

    public PlayerSkill PlayerSkill { get; set; }

    public AuctionPlayerStatus Status { get; set; }

    public int SoldPrice { get; set; }

    public string? SoldTo { get; set; }
}
