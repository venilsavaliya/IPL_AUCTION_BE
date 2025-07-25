using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.InningState;

namespace IplAuction.Service.Interface;

public interface IInningStateService
{
    Task<InningState> GetByIdAsync(int id);
    Task<List<InningState>> GetByMatchIdAsync(int matchId);
    Task<InningState> AddAsync(InningStateRequestModel state);
    Task UpdateAsync(UpdateInningStateRequest state);
    Task DeleteAsync(int id);
    Task SwapStrikeAsync(int matchId, int inningNumber);
    Task UpdateBowlerAsync(int matchId, int inningNumber, int? bowlerId);
    Task RemoveBatsmanAsync(int matchId, int inningNumber, int batsmanId);
    Task<InningState?> GetInningState(int matchId, int inningNumber);
} 