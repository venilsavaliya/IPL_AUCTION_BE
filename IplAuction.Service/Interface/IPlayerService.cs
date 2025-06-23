using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Player;
using Microsoft.AspNetCore.Http;

namespace IplAuction.Service.Interface;

public interface IPlayerService
{
    Task<List<PlayerResponseModel>> GetAllPlayersAsync();
    Task<PlayerResponseModel> GetPlayerByIdAsync(int id);
    Task AddPlayerAsync(AddPlayerRequest player);
    Task UpdatePlayerAsync(UpdatePlayerRequest player);
    Task DeletePlayerAsync(int id);
    Task<PaginatedResult<PlayerResponseModel>> GetPlayersAsync(PlayerFilterParams filterParams);
    Task ImportPlayersFromCsvAsync(IFormFile file);
}
