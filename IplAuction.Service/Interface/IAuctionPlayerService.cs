using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.AuctionPlayer;

namespace IplAuction.Service.Interface;

public interface IAuctionPlayerService
{
    Task AddAuctionPlayer(AddAuctionPlayerRequest auctionPlayer);

    Task<List<int>> GetAllAuctionedPlayerIds(int auctionId);

    Task MarkPlayerSold(AddAuctionPlayerRequest request);

    Task MarkPlayerUnsold(AddAuctionPlayerRequest request);

    PaginatedResult<AuctionPlayerDetail> GetAuctionPlayerDetailList(AuctionPlayerFilterParams request);
}
