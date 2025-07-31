using IplAuction.Entities.ViewModels.PlayerMatchState;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;
using IplAuction.Entities.ViewModels.Match;
using IplAuction.Entities.ViewModels.ScoringRules;
using IplAuction.Entities.Enums;

namespace IplAuction.Service.Implementations;

public class PlayerMatchStateService(IPlayerMatchStateRepository playerMatchStateRepository, IMatchService matchService, IScoringRulesService scoringRulesService) : IPlayerMatchStateService
{

    private readonly IPlayerMatchStateRepository _playerMatchStateRepository = playerMatchStateRepository;
    private readonly IMatchService _matchService = matchService;
    private readonly IScoringRulesService _scoringRuleService = scoringRulesService;

    public async Task AddPlayerMatchState(AddPlayerMatchStateRequest request)
    {
        var playerMatchState = new PlayerMatchStates
        {
            PlayerId = request.PlayerId,
            MatchId = request.MatchId,
            TeamId = request.TeamId,
            Fours = request.Fours,
            Sixes = request.Sixes,
            Runs = request.Runs,
            Wickets = request.Wickets,
            MaidenOvers = request.MaidenOvers,
            Catches = request.Catches,
            Stumpings = request.Stumpings,
        };
        await _playerMatchStateRepository.AddAsync(playerMatchState);
        await _playerMatchStateRepository.SaveChangesAsync();
    }

    public async Task<List<PlayerMatchStateResponse>> GetPlayerMatchStates(PlayerMatchStateRequestParams request)
    {
        List<PlayerMatchStates> data = await _playerMatchStateRepository.GetAllWithEagerLoadAndFilterAsync(p => p.MatchId == request.MatchId && p.TeamId == request.TeamId, p => p.Player);
        List<PlayerMatchStateResponse> response = data.Select(p => new PlayerMatchStateResponse(p)).ToList();
        return response;
    }

    public async Task UpdatePlayerMatchState(List<UpdatePlayerMatchStateRequest> request)
    {
        // existing player match states
        List<PlayerMatchStates> existingPlayerMatchStates = await _playerMatchStateRepository.GetAllWithEagerLoadAndFilterAsync(p => p.MatchId == request[0].MatchId);

        var existingPlayersMap = existingPlayerMatchStates.ToDictionary(p => p.Id);
        var newPlayersMap = request.Where(p => p.Id != 0).ToDictionary(p => p.Id);

        // handle Update

        foreach (var item in request.Where(p => p.Id != 0))
        {
            if (existingPlayersMap.ContainsKey(item.Id))
            {
                var existingPlayer = existingPlayersMap[item.Id];
                existingPlayer.Fours = item.Fours;
                existingPlayer.Sixes = item.Sixes;
                existingPlayer.Runs = item.Runs;
                existingPlayer.Wickets = item.Wickets;
                existingPlayer.MaidenOvers = item.MaidenOvers;
                existingPlayer.Catches = item.Catches;
                existingPlayer.Stumpings = item.Stumpings;
                existingPlayer.RunOuts = item.RunOuts;
            }
        }

        //  Handle Adds

        var newPlayers = request.Where(p => p.Id == 0).Select(p => new PlayerMatchStates
        {
            PlayerId = p.PlayerId,
            MatchId = p.MatchId,
            TeamId = p.TeamId,
            Fours = p.Fours,
            Sixes = p.Sixes,
            Runs = p.Runs,
            Wickets = p.Wickets,
            MaidenOvers = p.MaidenOvers,
            Catches = p.Catches,
            Stumpings = p.Stumpings,
            RunOuts = p.RunOuts,
        }).ToList();

        await _playerMatchStateRepository.AddRangeAsync(newPlayers);

        // Handle Deletes

        var toDelete = existingPlayerMatchStates.Where(p => !request.Select(r => r.Id).Contains(p.Id)).ToList();

        _playerMatchStateRepository.DeleteRange(toDelete);

        await _playerMatchStateRepository.SaveChangesAsync();

    }

    public async Task<MatchPointsResponseModel> GetMatchPoints(int matchId)
    {
        MatchResponse match = await _matchService.GetMatchById(matchId);

        List<PlayerMatchStates> teamAplayerMatchStates = await _playerMatchStateRepository.GetAllWithEagerLoadAndFilterAsync(p => p.MatchId == matchId && p.TeamId == match.TeamAId,p=>p.Player);
        List<PlayerMatchStates> teamBplayerMatchStates = await _playerMatchStateRepository.GetAllWithEagerLoadAndFilterAsync(p => p.MatchId == matchId && p.TeamId == match.TeamBId,p=>p.Player);

        List<ScoringRuleResponse> scoringRules = await _scoringRuleService.GetAllScoringRules();

        var scoringRulesMap = scoringRules.ToDictionary(s => s.EventType, s => s.Points);

        List<PlayerMatchStatesForMatchPointsResponse> teamAPlayers = teamAplayerMatchStates.Select(p =>
        {
            int totalPoints = 0;

            totalPoints += p.Fours * GetPoints(scoringRulesMap, CricketEventType.Four) + p.Fours * 4;
            totalPoints += p.Sixes * GetPoints(scoringRulesMap, CricketEventType.Six) + p.Sixes * 6;
            totalPoints += p.Runs * GetPoints(scoringRulesMap, CricketEventType.Run);
            totalPoints += p.Wickets * GetPoints(scoringRulesMap, CricketEventType.Wicket);
            totalPoints += p.Catches * GetPoints(scoringRulesMap, CricketEventType.Catch);
            totalPoints += p.Stumpings * GetPoints(scoringRulesMap, CricketEventType.Stumping);
            totalPoints += p.RunOuts * GetPoints(scoringRulesMap, CricketEventType.RunOut);
            totalPoints += p.RunOuts * GetPoints(scoringRulesMap, CricketEventType.MaidenOver);

            return new PlayerMatchStatesForMatchPointsResponse
            {
                Id = p.Id,
                PlayerId = p.PlayerId,
                MatchId = p.MatchId,
                Fours = p.Fours,
                Sixes = p.Sixes,
                Runs = p.Runs,
                Wickets = p.Wickets,
                MaidenOvers = p.MaidenOvers,
                Catches = p.Catches,
                Stumpings = p.Stumpings,
                RunOuts = p.RunOuts,
                ImageUrl = p.Player?.Image,
                Name = p.Player?.Name??"N/A",
                TotalPoints = totalPoints
            };

        }).ToList();

        List<PlayerMatchStatesForMatchPointsResponse> teamBPlayers = teamBplayerMatchStates.Select(p =>
        {
            int totalPoints = 0;

            totalPoints += p.Fours * GetPoints(scoringRulesMap, CricketEventType.Four) + p.Fours * 4;
            totalPoints += p.Sixes * GetPoints(scoringRulesMap, CricketEventType.Six) + p.Sixes * 6;
            totalPoints += p.Runs * GetPoints(scoringRulesMap, CricketEventType.Run);
            totalPoints += p.Wickets * GetPoints(scoringRulesMap, CricketEventType.Wicket);
            totalPoints += p.Catches * GetPoints(scoringRulesMap, CricketEventType.Catch);
            totalPoints += p.Stumpings * GetPoints(scoringRulesMap, CricketEventType.Stumping);
            totalPoints += p.RunOuts * GetPoints(scoringRulesMap, CricketEventType.RunOut);
            totalPoints += p.RunOuts * GetPoints(scoringRulesMap, CricketEventType.MaidenOver);

            return new PlayerMatchStatesForMatchPointsResponse
            {
                Id = p.Id,
                PlayerId = p.PlayerId,
                MatchId = p.MatchId,
                Fours = p.Fours,
                Sixes = p.Sixes,
                Runs = p.Runs,
                Wickets = p.Wickets,
                MaidenOvers = p.MaidenOvers,
                Catches = p.Catches,
                Stumpings = p.Stumpings,
                RunOuts = p.RunOuts,
                ImageUrl = p.Player?.Image,
                Name = p.Player?.Name??"N/A",
                TotalPoints = totalPoints
            };

        }).ToList();

        MatchPointsResponseModel matchPointsResponse = new()
        {
            MatchId = matchId,
            TeamAId = match.TeamAId,
            TeamBId = match.TeamBId,
            TeamAName = match.TeamAName,
            TeamBName = match.TeamBName,
            TeamAPlayerMatchState = teamAPlayers,
            TeamBPlayerMatchState = teamBPlayers,
            TeamAPoints = teamAPlayers.Sum(p => p.TotalPoints),
            TeamBPoints = teamBPlayers.Sum(p => p.TotalPoints)
        };


        return matchPointsResponse;
    }

    // Safe rule lookup
    private int GetPoints(Dictionary<CricketEventType, int> map, CricketEventType type)
    {
        return map.TryGetValue(type, out var pts) ? pts : 0;
    }
}
