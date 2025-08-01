namespace IplAuction.Entities.ViewModels.Match;

public class MatchRequest
{
    public int TeamAId { get; set; }

    public int TeamBId { get; set; }

    public DateTime StartDate { get; set; }

    public int SeasonId { get; set; }

}
