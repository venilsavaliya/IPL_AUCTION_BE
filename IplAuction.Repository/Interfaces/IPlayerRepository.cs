using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Player;

namespace IplAuction.Repository.Interfaces;

public interface IPlayerRepository : IGenericRepository<Player>
{
    Task<PaginatedResult<PlayerResponseModel>> GetFilteredPlayersAsync(PlayerFilterParams filterParams);

    Task AddPlayersAsync(List<Player> players);

    Task<PlayerResponseModel> GetRadomUnAuctionedPlayer(int auctionId);
    
    Dictionary<string, int> GetPlayerNameIdDictionary();
}
