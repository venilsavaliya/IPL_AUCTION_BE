using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Bid;

namespace IplAuction.Service.Interface;

public interface IBidService
{
    Task<ApiResponse<object>> PlaceBid(PlaceBidRequestModel request);
    Task PlaceOfflineBid(PlaceBidRequestModel request);
    Task<BidResponseModel> GetLatestBidByAuctionId(LatestBidRequestModel request);
}
