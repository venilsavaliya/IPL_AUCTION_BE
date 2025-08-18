namespace IplAuction.Entities.ViewModels.PlayerMatchState;

public class PlayerMatchStateResponse
{
    public PlayerMatchStateResponse() { }
    public PlayerMatchStateResponse(Models.PlayerMatchStates p)
    {
        Id = p.Id;
        PlayerId = p.PlayerId;
        MatchId = p.MatchId;
        Name = p.Player.Name;
        TeamId = p.TeamId;
        Fours = p.Fours;
        Sixes = p.Sixes;
        Runs = p.Runs;
        Wickets = p.Wickets;
        MaidenOvers = p.MaidenOvers;
        Catches = p.Catches;
        Stumpings = p.Stumpings;
        RunOuts = p.RunOuts;
        OrderNumber = p.OrderNumber;
    }
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MatchId { get; set; }
    public int TeamId { get; set; }
    public int Fours { get; set; }
    public int Sixes { get; set; }
    public int Runs { get; set; }
    public int Wickets { get; set; }
    public int MaidenOvers { get; set; }
    public int Catches { get; set; }
    public int Stumpings { get; set; }
    public int RunOuts { get; set; }
    public int OrderNumber { get; set; }
}
