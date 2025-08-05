using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Player;
using Microsoft.AspNetCore.Http;

namespace IplAuction.Service.Interface;

public interface IPlayerService
{
    Task<PlayerResponseDetailModel> GetPlayerDetailByIdAsync(int id);
    Task<PlayerResponseModel> GetPlayerByIdAsync(int id);
    Task AddPlayerAsync(AddPlayerRequest player);
    Task UpdatePlayerAsync(UpdatePlayerRequest player);
    Task UpdatePlayerStatusAsync(UpdatePlayerStatusRequest request);
    Task DeletePlayerAsync(int id);
    Task<PaginatedResult<PlayerResponseModel>> GetPlayersAsync(PlayerFilterParams filterParams);
    Task ImportPlayersFromCsvAsync(IFormFile file);
    Task<PlayerResponseModel> GetRadomUnAuctionedPlayer(int auctionId);
    Task<List<PlayerIdName>> GetAllPlayerIdNameAsync();
    Task<List<PlayerIdName>> GetPlayersByTeamIdAsync(int teamId);
    Task<List<PlayerSummary>> GetPlayerSummaryAsync(int teamId);
    Dictionary<string, int> GetPlayerNameIdDictionary();
}
