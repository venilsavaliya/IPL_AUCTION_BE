using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Repository.Implementations;

public class AuctionRepository(IplAuctionDbContext context) : GenericRepository<Auction>(context), IAuctionRepository
{
    public async Task<PaginatedResult<Auction>> GetFilteredAuctionsAsync(AuctionFilterModel auctionFilter)
{
    var query = _dbSet.AsQueryable();

    // Search by title 
    if (!string.IsNullOrEmpty(auctionFilter.Search))
    {
        query = query.Where(a => a.Title.Contains(auctionFilter.Search, StringComparison.CurrentCultureIgnoreCase));
    }

    // Filter by status
    if (auctionFilter.Status.HasValue)
    {
        query = query.Where(a => a.AuctionStatus == auctionFilter.Status);
    }

    // Filter by date range
    if (auctionFilter.StartDate.HasValue && auctionFilter.EndDate.HasValue)
    {
        query = query.Where(a => a.StartDate >= auctionFilter.StartDate && a.StartDate <= auctionFilter.EndDate);
    }

    return await query.ToPaginatedListAsync(auctionFilter.Pagination);
}

}
