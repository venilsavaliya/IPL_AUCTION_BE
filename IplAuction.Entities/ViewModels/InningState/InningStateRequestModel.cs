namespace IplAuction.Entities.ViewModels.InningState;

public class InningStateRequestModel
{
    public int MatchId { get; set; }
    public int InningNumber { get; set; }
    public int StrikerId { get; set; }
    public int NonStrikerId { get; set; }
    public int BowlerId { get; set; }
    public int BattingTeamId { get; set; }
    public int BowlingTeamId { get; set; }
}
