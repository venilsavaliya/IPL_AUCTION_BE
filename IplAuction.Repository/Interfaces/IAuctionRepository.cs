using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Auction;

namespace IplAuction.Repository.Interfaces;

public interface IAuctionRepository : IGenericRepository<Auction>
{
    Task<AuctionResponseModel> GetAuctionById(int id);
    Task<PaginatedResult<AuctionResponseModel>> GetFilteredAuctionsAsync(AuctionFilterParam filterParams);
}
