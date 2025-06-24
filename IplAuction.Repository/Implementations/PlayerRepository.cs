using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Helper;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Player;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace IplAuction.Repository.Implementations;

public class PlayerRepository(IplAuctionDbContext context) : GenericRepository<Player>(context), IPlayerRepository
{
    public async Task<PaginatedResult<PlayerResponseModel>> GetFilteredPlayersAsync(PlayerFilterParams filterParams)
    {
        var query = _context.Players.Include(u => u.Team).Where(p => p.IsDeleted != true).AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(filterParams.Search))
        {
            string search = filterParams.Search.ToLower();

            query = query.Where(u =>
                u.Name.ToLower().Contains(search));
        }

        // Filtering Role
        if (!string.IsNullOrEmpty(filterParams.Skill))
        {
            string role = filterParams.Skill.ToLower();

            if (Enum.TryParse<PlayerSkill>(filterParams.Skill, true, out var skillEnum))
            {
                query = query.Where(u => u.Skill == skillEnum);
            }
        }

        //Filtering Team
        if (filterParams.TeamId != null && filterParams.TeamId != 0)
        {
            query = query.Where(u => u.TeamId == filterParams.TeamId);
        }

        //Filtering Player Status
        if (filterParams.ActiveStatus != null)
        {
            query = query.Where(u => u.IsActive == filterParams.ActiveStatus);
        }

        // Sorting
        var allowedSorts = new[] { "Name", "Id", "Age", "Skill", "BasePrice", "TeamName", "Country" };
        var sortBy = allowedSorts.Contains(filterParams.SortBy) ? filterParams.SortBy : "Name";
        var sortDirection = filterParams.SortDirection?.ToLower() == "desc" ? "desc" : "asc";

        var sortProperty = sortBy == "TeamName" ? "Team.Name" : sortBy;

        query = query.OrderBy($"{sortProperty} {sortDirection}");

        PaginationParams paginationParams = new()
        {
            PageNumber = filterParams.PageNumber,
            PageSize = filterParams.PageSize
        };

        // Pagination

        PaginatedResult<PlayerResponseModel> paginatedResult = await query.ToPaginatedListAsync(paginationParams, u => new PlayerResponseModel
        {
            PlayerId = u.Id,
            Name = u.Name,
            ImageUrl = u.Image,
            TeamName = u.Team.Name,
            Skill = u.Skill,
            Age = CalculateAge.CalculateAgeFromDbo(u.DateOfBirth),
            Country = u.Country,
            IsActive = u.IsActive,
            BasePrice = u.BasePrice
        });

        return paginatedResult;
    }

    public async Task AddPlayersAsync(List<Player> players)
    {
        await _context.Players.AddRangeAsync(players);
        await _context.SaveChangesAsync();
    }
}