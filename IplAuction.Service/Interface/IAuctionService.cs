using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Entities.ViewModels.AuctionPlayer;
using IplAuction.Entities.ViewModels.Player;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Entities.ViewModels.UserTeam;

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

    Task MarkPlayerSold(AddUserTeamRequestModel request);

    Task MarkStatusToStart(int auctionId);

    Task MarkPlayerUnSold(AddAuctionPlayerRequest request);

    Task<PaginatedResult<UserAuctionResponseModel>> GetAllJoinedAuctionsOfUser(UserAuctionFilterParam filterParams);

    Task<int> GetSeasonIdFromAuctionId(int auctionId);

    Task MarkAuctionAsCompleted(int auctionId);

    Task MarkStatusToReshuffling(int auctionId);
}
