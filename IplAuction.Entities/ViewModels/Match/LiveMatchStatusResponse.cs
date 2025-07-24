namespace IplAuction.Entities.ViewModels.Match;

public class LiveMatchStatusResponse
{
    public int MatchId { get; set; }
    public string MatchStatus { get; set; } = null!;
    public string TeamA { get; set; } = null!;
    public string TeamB { get; set; } = null!;
    public int InningNumber { get; set; }
    public int TotalRuns { get; set; }
    public int TotalWickets { get; set; }
    public double Overs { get; set; } // e.g. , 12.3
    public int? Target { get; set; } // for 2nd innings
    public double? RequiredRunRate { get; set; } // for 2nd innings
    public double RunRate { get; set; }
    public List<BatsmanStatus> CurrentBatsmen { get; set; } = [];
    public BowlerStatus CurrentBowler { get; set; } = new BowlerStatus();
    public List<BallSummary> RecentBalls { get; set; } = [];
}

public class BatsmanStatus
{
    public int PlayerId { get; set; }
    public string Name { get; set; } = null!;
    public int Runs { get; set; }
    public int Balls { get; set; }
    public int Fours { get; set; }
    public int Sixes { get; set; }
    public bool IsOnStrike { get; set; }
}

public class BowlerStatus
{
    public int PlayerId { get; set; }
    public string Name { get; set; } = null!;
    public double Overs { get; set; }
    public int RunsConceded { get; set; }
    public int Wickets { get; set; }
}

public class BallSummary
{
    public int OverNumber { get; set; }
    public int BallNumber { get; set; }
    public string Result { get; set; } 
    public bool IsLegalDelivery { get; set; }
}