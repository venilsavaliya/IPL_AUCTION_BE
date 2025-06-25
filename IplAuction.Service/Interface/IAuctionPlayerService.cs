using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Service.Interface;

public interface IAuctionPlayerService
{
    Task AddAuctionPlayer(ManageAuctionPlayerRequest auctionPlayer);
}
