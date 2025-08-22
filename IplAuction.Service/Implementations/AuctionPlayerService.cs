using IplAuction.Entities.DTOs;
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
        AuctionPlayer? auctionPlayer = await _auctionPlayerRepo.GetWithFilterAsync(ap => ap.PlayerId == request.PlayerId && ap.AuctionId == request.AuctionId);
        if (auctionPlayer != null)
        {
            auctionPlayer.IsAuctioned = false;
            auctionPlayer.IsSold = false;

            await _auctionPlayerRepo.SaveChangesAsync();
        }
        else
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

    public async Task MarkPlayerSold(AddAuctionPlayerRequest request)
    {
        AuctionPlayer? existingAuctionPlayer = await _auctionPlayerRepo.GetWithFilterAsync(ap => ap.AuctionId == request.AuctionId && ap.PlayerId == request.PlayerId);

        if (existingAuctionPlayer != null)
        {
            existingAuctionPlayer.IsAuctioned = true;
            existingAuctionPlayer.IsSold = true;

            await _auctionPlayerRepo.SaveChangesAsync();
        }
        else
        {
            var entity = new AuctionPlayer
            {
                AuctionId = request.AuctionId,
                PlayerId = request.PlayerId,
                IsAuctioned = true,
                IsSold = true,
            };

            await _auctionPlayerRepo.AddAsync(entity);
            await _auctionPlayerRepo.SaveChangesAsync();
        }
    }

    public async Task<List<int>> GetAllAuctionedPlayerIds(int auctionId)
    {
        var auctionPlayers = await _auctionPlayerRepo.GetAllWithFilterAsync(ap => ap.AuctionId == auctionId);
        return auctionPlayers.Select(ap => ap.PlayerId).ToList();
    }

    public async Task MarkPlayerUnsold(AddAuctionPlayerRequest request)
    {
        AuctionPlayer? auctionPlayer = await _auctionPlayerRepo.GetWithFilterAsync(ap => ap.AuctionId == request.AuctionId && ap.PlayerId == request.PlayerId);

        if (auctionPlayer == null)
        {
            var entity = new AuctionPlayer
            {
                AuctionId = request.AuctionId,
                PlayerId = request.PlayerId,
                IsSold = true,
                IsAuctioned = true
            };

            await _auctionPlayerRepo.AddAsync(entity);
        }
        else
        {
            auctionPlayer.IsSold = false;

            auctionPlayer.IsAuctioned = true;
        }

        await _auctionPlayerRepo.SaveChangesAsync();
    }

    public PaginatedResult<AuctionPlayerDetail> GetAuctionPlayerDetailList(AuctionPlayerFilterParams request)
    {
        return _auctionPlayerRepo.GetAuctionPlayerDetailList(request);
    }

    // public async Task<AuctionPlayer?> GetExistingAuctionPlayer(AddAuctionPlayerRequest request)
    // {
    //     AuctionPlayer? ap = await _auctionPlayerRepo.GetWithFilterAsync(ap => ap.AuctionId == request.AuctionId && ap.PlayerId == request.PlayerId);
    // }

}
