using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.InningState;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class InningStateService(IInningStateRepository repo, IMatchRepository matchRepository) : IInningStateService
{
    private readonly IInningStateRepository _repo = repo;

    private readonly IMatchRepository _matchRepository = matchRepository;

    public async Task<InningState> GetByIdAsync(int id)
    {
        return await _repo.GetWithFilterAsync(i => i.Id == id) ?? throw new NotFoundException(nameof(InningState));
    }

    public async Task<List<InningState>> GetByMatchIdAsync(int matchId)
    {
        return await _repo.GetAllWithFilterAsync(i => i.MatchId == matchId);
    }

    public async Task<InningState> AddAsync(InningStateRequestModel state)
    {
        var match = await _matchRepository.GetWithFilterAsync(m => m.Id == state.MatchId) ?? throw new NotFoundException(nameof(Match));
        match.InningNumber = state.InningNumber;

        InningState inningState = new InningState
        {
            MatchId = state.MatchId,
            InningNumber = state.InningNumber,
            StrikerId = state.StrikerId,
            NonStrikerId = state.NonStrikerId,
            BowlerId = state.BowlerId
        };
        
        await _repo.AddAsync(inningState);
        await _repo.SaveChangesAsync();
        return inningState;
    }

    public async Task UpdateAsync(UpdateInningStateRequest state)
    {
        var inningState = await _repo.GetWithFilterAsync(i => i.MatchId == state.MatchId && i.InningNumber == state.InningNumber) ?? throw new NotFoundException(nameof(InningState));
        inningState.MatchId = state.MatchId;
        inningState.InningNumber = state.InningNumber;
        inningState.StrikerId = state.StrikerId;
        inningState.NonStrikerId = state.NonStrikerId;
        inningState.BowlerId = state.BowlerId;

        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var state = await _repo.GetWithFilterAsync(i => i.Id == id);
        if (state != null)
        {
            _repo.Delete(state);
            await _repo.SaveChangesAsync();
        }
    }

    public async Task SwapStrikeAsync(int matchId, int inningNumber)
    {
        var state = await _repo.GetWithFilterAsync(i => i.MatchId == matchId && i.InningNumber == inningNumber)
            ?? throw new NotFoundException(nameof(InningState));
        // Swap striker and non-striker
        var temp = state.StrikerId;
        state.StrikerId = state.NonStrikerId;
        state.NonStrikerId = temp;
        
        await _repo.SaveChangesAsync();
    }

    public async Task UpdateBowlerAsync(int matchId, int inningNumber, int? bowlerId)
    {
        var state = await _repo.GetWithFilterAsync(i => i.MatchId == matchId && i.InningNumber == inningNumber)
            ?? throw new NotFoundException(nameof(InningState));
        state.BowlerId = bowlerId ?? 0;
        await _repo.SaveChangesAsync();
    }

    public async Task RemoveBatsmanAsync(int matchId, int inningNumber, int batsmanId)
    {
        var state = await _repo.GetWithFilterAsync(i => i.MatchId == matchId && i.InningNumber == inningNumber)
            ?? throw new NotFoundException(nameof(InningState));
        if (state.StrikerId == batsmanId)
            state.StrikerId = 0;
        else if (state.NonStrikerId == batsmanId)
            state.NonStrikerId = 0;
        await _repo.SaveChangesAsync();
    }

    public async Task<InningState?> GetInningState(int matchId, int inningNumber)
    {
        return await _repo.GetWithFilterAsync(i => i.MatchId == matchId && i.InningNumber == inningNumber);
    }
}