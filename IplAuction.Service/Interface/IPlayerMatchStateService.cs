using IplAuction.Entities.ViewModels.PlayerMatchState;

namespace IplAuction.Service.Interface;

public interface IPlayerMatchStateService
{
    Task AddPlayerMatchState(AddPlayerMatchStateRequest request);
    Task<List<PlayerMatchStateResponse>> GetPlayerMatchStates(PlayerMatchStateRequestParams request);
    Task UpdatePlayerMatchState(List<UpdatePlayerMatchStateRequest> request);
}
