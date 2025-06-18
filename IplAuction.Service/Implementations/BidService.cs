using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Bid;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class BidService(IGenericRepository<Bid> bidRepository, IGenericRepository<AuctionPlayer> auctionPlayerRepo, IGenericRepository<Auction> auctionRepo, ICurrentUserService currentUser, IGenericRepository<AuctionParticipants> auctionParticipantsRepo) : IBidService
{
    private readonly IGenericRepository<Bid> _bidRepository = bidRepository;
    private readonly IGenericRepository<AuctionPlayer> _auctionPlayerRepo = auctionPlayerRepo;
    private readonly IGenericRepository<Auction> _auctionRepo = auctionRepo;
    private readonly IGenericRepository<AuctionParticipants> _auctionParticipantsRepo = auctionParticipantsRepo;
    private readonly ICurrentUserService _currentUser = currentUser;

    public async Task<ApiResponse<object>> PlaceBid(PlaceBidRequestModel request)
    {
        int? UserId = _currentUser.UserId;

        var responseBuilder = ApiResponseBuilder.With<object>();

        Auction auction = await _auctionRepo.FindAsync(request.AuctionId) ?? throw new NotFoundException(nameof(Auction));

        AuctionPlayer auctionPlayer = await _auctionPlayerRepo.GetWithFilterAsync(ap => ap.PlayerId == request.PlayerId) ?? throw new NotFoundException(nameof(AuctionPlayer));

        if (auction.AuctionStatus != AuctionStatus.Live)
        {
            return responseBuilder
            .SetStatusCodeAndMessage(400, Messages.AuctionNotLive)
            .Build();
        }

        if (auctionPlayer.IsSold)
        {
            return responseBuilder
            .SetStatusCodeAndMessage(400, Messages.PlayerAlreadySold)
            .Build();
        }

        if (request.BidAmount < auction.CurrentBid + auction.MinimumBidIncreament)
        {
            return responseBuilder
           .SetStatusCodeAndMessage(400, Messages.BidMustHigher)
           .Build();
        }

        AuctionParticipants user = await _auctionParticipantsRepo.GetWithFilterAsync(u => u.UserId == UserId) ?? throw new NotFoundException(nameof(AuctionParticipants));

        if (request.BidAmount <= user!.PurseBalance)
        {
            return responseBuilder
            .SetStatusCodeAndMessage(400, Messages.InsufficientBalance)
            .Build();
        }

        Bid bid = new()
        {
            AuctionId = request.AuctionId,
            PlayerId = request.PlayerId,
            UserId = (int)UserId,
            Amount = request.BidAmount,
            PlacedAt = request.PlacedAt
        };

        await _bidRepository.AddAsync(bid);

        await _bidRepository.SaveChangesAsync();

        return responseBuilder
            .SetStatusCodeAndMessage(201, Messages.BidAdded)
            .Build();
    }
}
