using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace IplAuction.Repository.Implementations;

public class AuctionRepository(IplAuctionDbContext context) : GenericRepository<Auction>(context), IAuctionRepository
{
    public async Task<PaginatedResult<AuctionResponseModel>> GetFilteredAuctionsAsync(AuctionFilterParam filterParams)
    {
        var query = _context.Auctions.Where(a=>a.IsDeleted != true).Include(u => u.UserTeams).AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(filterParams.Search))
        {
            string search = filterParams.Search.ToLower();

            query = query.Where(u =>
                u.Title.ToLower().Contains(search));
        }

        // Filtering Role
        if (!string.IsNullOrEmpty(filterParams.Status))
        {
            if (Enum.TryParse<AuctionStatus>(filterParams.Status, true, out var statusEnum))
            {
                query = query.Where(u => u.AuctionStatus == statusEnum);
            }
        }

        //Filtering Date
        if (filterParams.FromDate.HasValue && filterParams.ToDate.HasValue)
        {
            var fromDateUtc = DateTime.SpecifyKind(filterParams.FromDate.Value, DateTimeKind.Utc);
            var toDateUtc = DateTime.SpecifyKind(filterParams.ToDate.Value, DateTimeKind.Utc);

            query = query.Where(u => u.StartDate >= fromDateUtc && u.StartDate <= toDateUtc);
        }

        // Sorting
        var allowedSorts = new[] { "Title", "Id", "StartDate" };
        var sortBy = allowedSorts.Contains(filterParams.SortBy) ? filterParams.SortBy : "Id";
        var sortDirection = filterParams.SortDirection?.ToLower() == "asc" ? "asc" : "desc";
        query = query.OrderBy($"{sortBy} {sortDirection}");

        PaginationParams paginationParams = new()
        {
            PageNumber = filterParams.PageNumber,
            PageSize = filterParams.PageSize
        };

        // Pagination
        PaginatedResult<AuctionResponseModel> paginatedResult = await query.ToPaginatedListAsync(paginationParams, u => new AuctionResponseModel
        {
            Id = u.Id,
            Title = u.Title,
            StartDate = u.StartDate,
            AuctionStatus = u.AuctionStatus,
            ManagerId = u.ManagerId,
            MaximumPurseSize = u.MaximumPurseSize,
            MinimumBidIncreament = u.MinimumBidIncreament,
            TotalTeams = u.UserTeams.Count()
        });

        return paginatedResult;
    }
}
