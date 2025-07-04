using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Entities.ViewModels.AuctionPlayer;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Service.Interface;

public interface IAuctionPlayerService
{
    Task AddAuctionPlayer(AddAuctionPlayerRequest auctionPlayer);

    Task<List<int>> GetAllAuctionedPlayerIds(int auctionId);
}
