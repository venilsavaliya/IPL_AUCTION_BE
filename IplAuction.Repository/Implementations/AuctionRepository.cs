using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace IplAuction.Repository.Implementations;

public class AuctionRepository(IplAuctionDbContext context) : GenericRepository<Auction>(context), IAuctionRepository
{
    public async Task<PaginatedResult<AuctionResponseModel>> GetFilteredAuctionsAsync(AuctionFilterParam filterParams)
    {
        var query = _context.Auctions.Include(a => a.AuctionParticipants).Where(a => a.IsDeleted != true).Include(u => u.UserTeams).AsQueryable();

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
        PaginatedResult<AuctionResponseModel> paginatedResult = await query.ToPaginatedListAsync(paginationParams, u => new AuctionResponseModel(u));

        return paginatedResult;
    }

    public async Task<AuctionResponseModel> GetAuctionById(int id)
    {
        Auction auction = await _context.Auctions.Include(a => a.AuctionParticipants).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted != true) ?? throw new NotFoundException(nameof(Auction));

        return new AuctionResponseModel(auction);
    }

    public async Task<PaginatedResult<UserAuctionResponseModel>> GetUsersAuctions(UserAuctionFilterParam filterParams)
    {
        var query = _context.Auctions.Where(u => u.IsDeleted != true).Select(u => new UserAuctionResponseModel
        {
            AuctionId = u.Id,
            UserId = filterParams.UserId,
            AuctionTitle = u.Title,
            AuctionStatus = u.AuctionStatus,
            StartTime = u.StartDate,
            AmountRemaining =u.MaximumPurseSize - u.UserTeams.Where(ut => ut.UserId == filterParams.UserId).Sum(ut => ut.Price),
            TotalPlayer = u.UserTeams.Count(ut => ut.UserId == filterParams.UserId)
        });

        // Search
        if (!string.IsNullOrWhiteSpace(filterParams.Search))
        {
            string search = filterParams.Search.ToLower();

            query = query.Where(u =>
                u.AuctionTitle.ToLower().Contains(search));
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

            query = query.Where(u => u.StartTime >= fromDateUtc && u.StartTime <= toDateUtc);
        }

        // Sorting Computed Fields
        var allowedCustomSort = new[] { "TotalPlayer", "AmountRemaining","AuctionTitle","StartTime"};

         var sortBy = allowedCustomSort.Contains(filterParams.SortBy) ? filterParams.SortBy : "AuctionId";
        var sortDirection = filterParams.SortDirection?.ToLower() == "asc" ? "asc" : "desc";
        query = query.OrderBy($"{sortBy} {sortDirection}");

        PaginationParams paginationParams = new()
        {
            PageNumber = filterParams.PageNumber,
            PageSize = filterParams.PageSize
        };

        // Pagination
        PaginatedResult<UserAuctionResponseModel> paginatedResult = await query.ToPaginatedListAsync(paginationParams, u => new UserAuctionResponseModel(u));

        return paginatedResult;
    }
}
