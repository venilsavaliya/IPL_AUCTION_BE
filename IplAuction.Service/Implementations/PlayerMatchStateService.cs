using IplAuction.Entities.ViewModels.PlayerMatchState;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;
using IplAuction.Entities.Exceptions;

namespace IplAuction.Service.Implementations;

public class PlayerMatchStateService : IPlayerMatchStateService
{

    private readonly IPlayerMatchStateRepository _playerMatchStateRepository;

    public PlayerMatchStateService(IPlayerMatchStateRepository playerMatchStateRepository)
    {
        _playerMatchStateRepository = playerMatchStateRepository;
    }

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
        foreach (var item in request)
        {
            if (item.Id == 0)
            {
                await AddPlayerMatchState(new AddPlayerMatchStateRequest
                {
                    PlayerId = item.PlayerId,
                    MatchId = item.MatchId,
                    TeamId = item.TeamId,
                    Fours = item.Fours,
                    Sixes = item.Sixes,
                    Runs = item.Runs,
                    Wickets = item.Wickets,
                    MaidenOvers = item.MaidenOvers,
                    Catches = item.Catches,
                    Stumpings = item.Stumpings,
                    RunOuts = item.RunOuts,
                });
                await _playerMatchStateRepository.SaveChangesAsync();
            }
            else
            {
                PlayerMatchStates playerMatchState = await _playerMatchStateRepository.GetWithFilterAsync(p => p.Id == item.Id) ?? throw new NotFoundException(nameof(PlayerMatchStates));

                playerMatchState.Fours = item.Fours;
                playerMatchState.Sixes = item.Sixes;
                playerMatchState.Runs = item.Runs;
                playerMatchState.Wickets = item.Wickets;
                playerMatchState.MaidenOvers = item.MaidenOvers;
                playerMatchState.Catches = item.Catches;
                playerMatchState.Stumpings = item.Stumpings;
                playerMatchState.RunOuts = item.RunOuts;
                await _playerMatchStateRepository.SaveChangesAsync();
            }
        }

    }
}
