namespace IplAuction.Entities.ViewModels.BallEvent;
using IplAuction.Entities.Enums;

public class MatchStates
{
    public int MatchId { get; set; }
    public int CurrentInnings { get; set; }
    public int BattingTeamId { get; set; }
    public int BowlingTeamId { get; set; }
    public string BattingTeamName { get; set; }
    public string BowlingTeamName { get; set; }
    public int TotalRuns { get; set; }
    public int TotalWickets { get; set; }
    public string Overs { get; set; }

    public List<Batsman> Batsmen { get; set; }

    public Bowler CurrentBowler {get;set;}

    public List<CurrentOverBall> CurrentOverBalls { get; set; }


}

public class Batsman
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }
    public int Runs { get; set; }
    public int Balls { get; set; }
    public bool IsStriker { get; set; }
}

public class Bowler
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }
    public string Overs { get; set; }
    public int Balls { get; set; }
    public int Runs { get; set; }
    public int Wickets { get; set; }
    public int MaidenOvers { get; set; }
}

public class CurrentOverBall
{
    public int BallNumber { get; set; }
    public int Runs { get; set; }
    public bool IsWicket {get;set;}
    public ExtraType? ExtraType {get;set;}
    public int? ExtraRuns {get;set;}
}

