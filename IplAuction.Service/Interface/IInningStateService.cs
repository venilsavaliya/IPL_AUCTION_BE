using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.InningState;

namespace IplAuction.Service.Interface;

public interface IInningStateService
{
    Task<InningState> GetByIdAsync(int id);
    Task<List<InningState>> GetByMatchIdAsync(int matchId);
    Task<InningState> AddAsync(InningStateRequestModel state);
    Task UpdateAsync(InningState state);
    Task DeleteAsync(int id);
    Task SwapStrikeAsync(int matchId, int inningNumber);
} 