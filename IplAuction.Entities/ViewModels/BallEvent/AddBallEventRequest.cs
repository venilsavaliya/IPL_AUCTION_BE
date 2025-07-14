using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.BallEvent;

public class AddBallEventRequest
{
    public int MatchId { get; set; }
    public int InningNumber { get; set; }
    public int OverNumber { get; set; }
    public int BallNumber { get; set; }

    public int BatsmanId { get; set; }
    public int NonStrikerId { get; set; }
    public int BowlerId { get; set; }

    public int RunsScored { get; set; }
    public ExtraType? ExtraType { get; set; }
    public int ExtraRuns { get; set; }

    public WicketType? WicketType { get; set; }
    public int? DismissedPlayerId { get; set; }
    public int? FielderId { get; set; }

}
