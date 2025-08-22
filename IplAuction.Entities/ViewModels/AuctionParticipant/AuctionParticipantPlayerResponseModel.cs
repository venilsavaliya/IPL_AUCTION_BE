using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.AuctionParticipant;

public class AuctionParticipantPlayerResponseModel
{
    public int UserId { get; set; }
    public int TotalPlayers { get; set; }
    public int TotalPoints { get; set; }
    public int TotalAmountSpent { get; set; }
    public List<ParticipantsPlayer> ParticipantsPlayers { get; set; } = [];
}

public class ParticipantsPlayer
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = null!;
    public int PlayerPoints { get; set; }
    public int PlayerPrice { get; set; }
    public string? PlayerImage { get; set; }
    public PlayerSkill PlayerSkill { get; set; }
    public int PlayerBoughtPrice { get; set; }
    public int PlayersTotalMatches { get; set; }
    public bool IsReshuffled { get; set; }
    public bool IsJoined { get; set; }
    public bool IsLeave { get; set; }
}
