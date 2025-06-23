using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Entities.ViewModels.Player;

namespace IplAuction.Service.Interface;

public interface IAuctionService
{
    Task<List<AuctionResponseModel>> GetAllAuctionAsync();

    Task<PaginatedResult<AuctionResponseModel>> GetAuctionsAsync(AuctionFilterParam filterParams);

    Task<AuctionResponseModel> GetAuctionByIdAsync(int id);

    Task AddAuctionAsync(AddAuctionRequestModel Auction);

    Task UpdateAuctionAsync(UpdateAuctionRequestModel Auction);

    Task<bool> DeleteAuctionAsync(int id);

    Task<bool> JoinAuctionAsync(int id);

    Task<PlayerResponseModel> GetRandomUnAuctionedPlayer(int auctionId);

    Task AddPlayerToAuction(ManageAuctionPlayerRequest request);
    
    Task RemovePlayerFromAuction(ManageAuctionPlayerRequest request);
}
