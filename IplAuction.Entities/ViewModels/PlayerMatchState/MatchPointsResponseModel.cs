using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.PlayerMatchState;

public class MatchPointsResponseModel
{
    public int MatchId { get; set; }
    public int TeamAId { get; set; }
    public int TeamBId { get; set; }
    public string TeamAName { get; set; } = null!;
    public string TeamBName { get; set; } = null!;
    public int TeamAPoints { get; set; }
    public int TeamBPoints { get; set; }
    public List<PlayerMatchStatesForMatchPointsResponse> TeamAPlayerMatchState { get; set; } = [];
    public List<PlayerMatchStatesForMatchPointsResponse> TeamBPlayerMatchState { get; set; } = [];
}

public class PlayerMatchStatesForMatchPointsResponse : PlayerMatchStateResponse
{
    public PlayerMatchStatesForMatchPointsResponse() { }
    public PlayerMatchStatesForMatchPointsResponse(PlayerMatchStates p) : base(p)
    {
        ImageUrl = p.Player?.Image;
    }
    public string? ImageUrl { get; set; }
    
    public int TotalPoints { get; set; }
}