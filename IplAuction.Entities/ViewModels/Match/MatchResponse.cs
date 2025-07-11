namespace IplAuction.Entities.ViewModels.Match;

public class MatchResponse
{   
    public int MatchId { get; set; }
    public int TeamAId { get; set; }

    public int TeamBId { get; set; }

    public string TeamAName { get; set; } = null!;

    public string TeamBName { get; set; } = null!;

    public DateTime StartDate { get; set; }
}
