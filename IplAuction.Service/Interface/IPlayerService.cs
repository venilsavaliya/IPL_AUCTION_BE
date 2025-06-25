using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Player;
using Microsoft.AspNetCore.Http;

namespace IplAuction.Service.Interface;

public interface IPlayerService
{
    Task<List<PlayerResponseModel>> GetAllPlayersAsync();
    Task<PlayerResponseDetailModel> GetPlayerByIdAsync(int id);
    Task AddPlayerAsync(AddPlayerRequest player);
    Task UpdatePlayerAsync(UpdatePlayerRequest player);
    Task UpdatePlayerStatusAsync(UpdatePlayerStatusRequest request);
    Task DeletePlayerAsync(int id);
    Task<PaginatedResult<PlayerResponseModel>> GetPlayersAsync(PlayerFilterParams filterParams);
    Task ImportPlayersFromCsvAsync(IFormFile file);
}
