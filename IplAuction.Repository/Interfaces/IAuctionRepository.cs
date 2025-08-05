using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Entities.ViewModels.User;

namespace IplAuction.Repository.Interfaces;

public interface IAuctionRepository : IGenericRepository<Auction>
{
    Task<AuctionResponseModel> GetAuctionById(int id);
    Task<PaginatedResult<AuctionResponseModel>> GetFilteredAuctionsAsync(AuctionFilterParam filterParams);
    Task<PaginatedResult<UserAuctionResponseModel>> GetUsersAuctions(UserAuctionFilterParam filterParams);

}
