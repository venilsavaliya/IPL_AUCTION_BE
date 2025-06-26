using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Team;
using IplAuction.Repository.Interfaces;
using System.Linq.Dynamic.Core;

namespace IplAuction.Repository.Implementations;

public class TeamRepository(IplAuctionDbContext context) : GenericRepository<Team>(context) , ITeamRepository
{
    public async Task<PaginatedResult<TeamResponseViewModel>> GetFilteredTeamsAsync(TeamFilterParam filterParams)
    {
        var query = _context.Teams.Where(u => u.IsDeleted != true).AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(filterParams.Search))
        {
            string search = filterParams.Search.ToLower();

            query = query.Where(u =>
                u.Name.ToLower().Contains(search));
        }

        // Sorting
        var allowedSorts = new[] { "Name" };
        var sortBy = allowedSorts.Contains(filterParams.SortBy) ? filterParams.SortBy : "Name";
        var sortDirection = filterParams.SortDirection?.ToLower() == "asc" ? "asc" : "desc";

        query = query.OrderBy($"{sortBy} {sortDirection}");

        PaginationParams paginationParams = new()
        {
            PageNumber = filterParams.PageNumber,
            PageSize = filterParams.PageSize
        };

        // Pagination

        PaginatedResult<TeamResponseViewModel> paginatedResult = await query.ToPaginatedListAsync(paginationParams, u => new TeamResponseViewModel(u));

        return paginatedResult;
    }
}
