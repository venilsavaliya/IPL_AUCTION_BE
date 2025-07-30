namespace IplAuction.Entities.Models;

public class PlayerMatchStates
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int MatchId { get; set; }
    public int TeamId { get; set; }
    public int Fours { get; set; }
    public int Sixes {get;set;}
    public int Runs {get;set;}
    public int Wickets {get;set;}
    public int MaidenOvers {get;set;}
    public int Catches {get;set;}
    public int Stumpings {get;set;}
    public int RunOuts {get;set;}
   
    public Player Player { get; set; }
    public Match Match { get; set; }
}
