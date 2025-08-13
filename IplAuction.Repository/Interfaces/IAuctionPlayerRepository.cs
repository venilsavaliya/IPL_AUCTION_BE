using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.AuctionPlayer;

namespace IplAuction.Repository.Interfaces;

public interface IAuctionPlayerRepository : IGenericRepository<AuctionPlayer>
{
    PaginatedResult<AuctionPlayerDetail> GetAuctionPlayerDetailList(AuctionPlayerFilterParams request);
}
