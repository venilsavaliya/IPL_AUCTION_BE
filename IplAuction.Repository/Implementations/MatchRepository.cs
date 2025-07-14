using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Match;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace IplAuction.Repository.Implementations;

public class MatchRepository(IplAuctionDbContext context) : GenericRepository<Match>(context), IMatchRepository
{
    public async Task<PaginatedResult<MatchResponse>> GetFilteredMatchAsync(MatchFilterParams filterParams)
    {
        var query = _context.Matches.Where(a => a.IsDeleted != true);

        // Search
        if (!string.IsNullOrWhiteSpace(filterParams.Search))
        {
            string search = filterParams.Search.ToLower();

            query = query.Where(m =>
                m.TeamA.Name.ToLower().Contains(search) || m.TeamB.Name.ToLower().Contains(search));
        }

        //Filtering Date
        if (filterParams.FromDate.HasValue && filterParams.ToDate.HasValue)
        {
            var fromDateUtc = DateTime.SpecifyKind(filterParams.FromDate.Value, DateTimeKind.Utc);
            var toDateUtc = DateTime.SpecifyKind(filterParams.ToDate.Value, DateTimeKind.Utc);

            query = query.Where(u => u.StartDate >= fromDateUtc && u.StartDate <= toDateUtc);
        }

        // Sorting
        var allowedSorts = new[] { "StartDate" };
        var sortBy = allowedSorts.Contains(filterParams.SortBy) ? filterParams.SortBy : "Id";
        var sortDirection = filterParams.SortDirection?.ToLower() == "asc" ? "asc" : "desc";
        query = query.OrderBy($"{sortBy} {sortDirection}");

        PaginationParams paginationParams = new()
        {
            PageNumber = filterParams.PageNumber,
            PageSize = filterParams.PageSize
        };

        // Pagination
        PaginatedResult<MatchResponse> paginatedResult = await query.ToPaginatedListAsync(paginationParams, u => new MatchResponse
        {
            TeamAId = u.TeamAId,
            TeamBId = u.TeamBId,
            TeamAName = u.TeamA.Name,
            TeamBName = u.TeamB.Name,
            MatchId = u.Id,
            StartDate = u.StartDate
        });

        return paginatedResult;
    }

    public async Task<MatchResponse> GetById(int id)
    {
        MatchResponse result = await _context.Matches.Where(m => m.Id == id && m.IsDeleted != true).Select(m => new MatchResponse
        {
            MatchId = m.Id,
            StartDate = m.StartDate,
            TeamAId = m.TeamAId,
            TeamBId = m.TeamBId,
            TeamAName = m.TeamA.Name,
            TeamBName = m.TeamB.Name
        }).FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Match));

        return result;
    }
}
