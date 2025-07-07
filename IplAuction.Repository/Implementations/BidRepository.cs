using IplAuction.Entities;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Bid;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IplAuction.Repository.Implementations;

public class BidRepository(IplAuctionDbContext context) : GenericRepository<Bid>(context), IBidRepository
{
    public async Task<Bid> GetLatestBidByAuctionId(LatestBidRequestModel request)
    {
        Bid bid = await _context.Bids.OrderByDescending(b => b.PlacedAt).FirstOrDefaultAsync(b => b.AuctionId == request.AuctionId && b.PlayerId == request.PlayerId) ?? throw new NotFoundException(nameof(Bid));

        return bid;
    }
}
