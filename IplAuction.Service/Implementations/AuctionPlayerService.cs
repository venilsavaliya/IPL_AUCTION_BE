using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class AuctionPlayerService(IGenericRepository<AuctionPlayer> auctionPlayerRepo) : IAuctionPlayerService
{
    private readonly IGenericRepository<AuctionPlayer> _auctionPlayerRepo = auctionPlayerRepo;

    public async Task AddAuctionPlayer(ManageAuctionPlayerRequest request)
    {
        var entity = new AuctionPlayer
        {
            AuctionId = request.AuctionId,
            PlayerId = request.PlayerId
        };

        await _auctionPlayerRepo.AddAsync(entity);
        await _auctionPlayerRepo.SaveChangesAsync();
    }
}
