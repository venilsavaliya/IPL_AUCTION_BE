using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Entities.ViewModels.AuctionPlayer;
using IplAuction.Entities.ViewModels.Player;
using IplAuction.Entities.ViewModels.User;

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

    Task<List<UserResponseViewModel>> GetAllTeamsOfAuction(int auctionId);

    Task<PlayerResponseModel> GetCurrentAuctionPlayer(int auctionId);

    Task SetCurrentPlayerForAuction(AuctionPlayerRequest request);

    Task RemoveCurrentPlayerFromAuction(int auctionId);

    Task<PaginatedResult<UserAuctionResponseModel>> GetAllJoinedAuctionsOfUser(UserAuctionFilterParam filterParams);

    // Task AddPlayerToAuction(ManageAuctionPlayerRequest request);

    // Task RemovePlayerFromAuction(ManageAuctionPlayerRequest request);
}
