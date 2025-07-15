using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.AuctionPlayer;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class AuctionPlayerService(IAuctionPlayerRepository auctionPlayerRepo) : IAuctionPlayerService
{
    private readonly IAuctionPlayerRepository _auctionPlayerRepo = auctionPlayerRepo;

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

    public async Task MarkPlayerUnsold(AddAuctionPlayerRequest request)
    {
        AuctionPlayer auctionPlayer = await _auctionPlayerRepo.GetWithFilterAsync(ap => ap.AuctionId == request.AuctionId && ap.PlayerId == request.PlayerId) ?? throw new NotFoundException(nameof(AuctionPlayer));

        auctionPlayer.IsSold = false;

        auctionPlayer.IsAuctioned = true;

        await _auctionPlayerRepo.SaveChangesAsync();
    }
}
