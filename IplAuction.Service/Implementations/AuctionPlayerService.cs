using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.AuctionPlayer;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class AuctionPlayerService(IGenericRepository<AuctionPlayer> auctionPlayerRepo) : IAuctionPlayerService
{
    private readonly IGenericRepository<AuctionPlayer> _auctionPlayerRepo = auctionPlayerRepo;

    public async Task AddAuctionPlayer(AddAuctionPlayerRequest request)
    {
        var entity = new AuctionPlayer
        {
            AuctionId = request.AuctionId,
            PlayerId = request.PlayerId
        };

        await _auctionPlayerRepo.AddAsync(entity);
        await _auctionPlayerRepo.SaveChangesAsync();
    }

    public async Task<List<int>> GetAllAuctionedPlayerIds(int auctionId)
    {
        var auctionPlayers = await _auctionPlayerRepo.GetAllWithFilterAsync(ap => ap.AuctionId == auctionId);
        return auctionPlayers.Select(ap => ap.PlayerId).ToList();
    }
}
