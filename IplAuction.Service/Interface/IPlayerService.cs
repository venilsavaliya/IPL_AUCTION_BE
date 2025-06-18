using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Player;

namespace IplAuction.Service.Interface;

public interface IPlayerService
{
    Task<List<PlayerResponseModel>> GetAllPlayersAsync();
    Task<PlayerResponseModel> GetPlayerByIdAsync(int id);
    Task AddPlayerAsync(AddPlayerRequest player);
    Task UpdatePlayerAsync(UpdatePlayerRequest player);
    Task DeletePlayerAsync(int id);
}
