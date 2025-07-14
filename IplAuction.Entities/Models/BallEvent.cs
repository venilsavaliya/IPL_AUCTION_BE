using IplAuction.Entities.Enums;
using IplAuction.Entities.ViewModels.BallEvent;

namespace IplAuction.Entities.Models;

public class BallEvent
{
    public BallEvent() { }

    public BallEvent(AddBallEventRequest request)
    {
        MatchId = request.MatchId;
        InningNumber = request.InningNumber;
        OverNumber = request.OverNumber;
        BallNumber = request.BallNumber;
        BatsmanId = request.BatsmanId;
        NonStrikerId = request.NonStrikerId;
        BowlerId = request.BowlerId;
        RunsScored = request.RunsScored;
        ExtraType = request.ExtraType;
        ExtraRuns = request.ExtraRuns;
        WicketType = request.WicketType;
        DismissedPlayerId = request.DismissedPlayerId;
        FielderId = request.FielderId;
    }


    public int Id { get; set; }
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

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public Match Match { get; set; } = null!;
    public Player Batsman { get; set; } = null!;
    public Player NonStriker { get; set; } = null!;
    public Player Bowler { get; set; } = null!;
    public Player? DismissedPlayer { get; set; }
    public Player? Fielder { get; set; } 
}

