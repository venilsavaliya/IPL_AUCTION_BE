using IplAuction.Entities.Enums;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Migrations;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.ScoringRules;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class MatchPointService(IMatchRepository matchRepository, IScoringRuleRepository scoringRuleRepository, IPlayerMatchStateRepository playerMatchStateRepository, IScoringRulesService scoringRulesService) : IMatchPointservice
{

    private readonly IMatchRepository _matchRepository = matchRepository;

    private readonly IScoringRuleRepository _scoringRuleRepository = scoringRuleRepository;

    private readonly IScoringRulesService _scoringRulesService = scoringRulesService;

    private readonly IPlayerMatchStateRepository _playerMatchStateRepository = playerMatchStateRepository;
    public async Task<int> GetTotalPointsOfAllPlayersBySeasonId(List<int> players, int seasonId)
    {


        int totalPoints = 0;
        foreach (int playerId in players)
        {
            List<int> matchIds = await _playerMatchStateRepository.GetAllWithFilterAsync(m => m.Match.SeasonId == seasonId && m.PlayerId == playerId, m => m.MatchId);
            foreach (int matchId in matchIds)
            {
                totalPoints += await GetPointOfPlayerInMatch(playerId, matchId);
            }
        }

        return totalPoints;

    }

    public async Task<int> GetPointOfPlayerInMatch(int playerId, int matchId)
    {

        PlayerMatchStates playerState = await _playerMatchStateRepository.GetWithFilterAsync(p => p.PlayerId == playerId && p.MatchId == matchId) ?? throw new NotFoundException(nameof(PlayerMatchStates));

        List<ScoringRuleResponse> scoringRules = await _scoringRulesService.GetAllScoringRules();

        var scoringRulesMap = scoringRules.ToDictionary(s => s.EventType, s => s.Points);

        int totalPoints = 0;

        totalPoints += playerState.Fours * GetPoints(scoringRulesMap, CricketEventType.Four);
        totalPoints += playerState.Sixes * GetPoints(scoringRulesMap, CricketEventType.Six);
        totalPoints += playerState.Runs * GetPoints(scoringRulesMap, CricketEventType.Run);
        totalPoints += playerState.Wickets * GetPoints(scoringRulesMap, CricketEventType.Wicket);
        totalPoints += playerState.Catches * GetPoints(scoringRulesMap, CricketEventType.Catch);
        totalPoints += playerState.Stumpings * GetPoints(scoringRulesMap, CricketEventType.Stumping);
        totalPoints += playerState.RunOuts * GetPoints(scoringRulesMap, CricketEventType.RunOut);
        totalPoints += playerState.MaidenOvers * GetPoints(scoringRulesMap, CricketEventType.MaidenOver);

        return totalPoints;

    }

    private int GetPoints(Dictionary<CricketEventType, int> map, CricketEventType type)
    {
        return map.TryGetValue(type, out var pts) ? pts : 0;
    }
}
