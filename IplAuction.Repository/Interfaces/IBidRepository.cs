using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Bid;

namespace IplAuction.Repository.Interfaces;

public interface IBidRepository : IGenericRepository<Bid>
{
    Task<Bid> GetLatestBidByAuctionId(LatestBidRequestModel request);
}
