using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.InningState;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class InningStateService : IInningStateService
{
    private readonly IInningStateRepository _repo;
    public InningStateService(IInningStateRepository repo)
    {
        _repo = repo;
    }

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

    public async Task UpdateAsync(InningState state)
    {
        _repo.Update(state);
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
}