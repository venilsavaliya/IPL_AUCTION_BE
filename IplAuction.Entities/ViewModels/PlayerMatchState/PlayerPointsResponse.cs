namespace IplAuction.Entities.ViewModels.PlayerMatchState;

public class PlayerPointsResponse
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int MatchId { get; set; }
    public int SeasonId { get; set; }
    public int Fours { get; set; }
    public int Sixes { get; set; }
    public int Runs { get; set; }
    public int Wickets { get; set; }
    public int MaidenOvers { get; set; }
    public int Catches { get; set; }
    public int Stumpings { get; set; }
    public int RunOuts { get; set; }
    public int TotalPoints { get; set; }
} 