using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;
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
        var query = _context.Players.Include(u => u.Team).AsQueryable();

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
        if (filterParams.TeamId != 0)
        {
            query = query.Where(u => u.TeamId == filterParams.TeamId);
        }

        // Sorting
        var allowedSorts = new[] { "Name", "Id", "DateOfBirth", "Skill" };
        var sortBy = allowedSorts.Contains(filterParams.SortBy) ? filterParams.SortBy : "Id";
        var sortDirection = filterParams.SortDirection?.ToLower() == "asc" ? "asc" : "desc";
        query = query.OrderBy($"{sortBy} {sortDirection}");

        PaginationParams paginationParams = new()
        {
            PageNumber = filterParams.PageNumber,
            PageSize = filterParams.PageSize
        };

        // Pagination

        PaginatedResult<PlayerResponseModel> paginatedResult = await query.ToPaginatedListAsync(paginationParams, u => new PlayerResponseModel
        {
            Id = u.Id,
            Name = u.Name,
            Image = u.Image,
            TeamId = u.TeamId,
            TeamName = u.Team.Name,
            Skill = u.Skill.ToString(),
            DateOfBirth = u.DateOfBirth,
            BasePrice = u.BasePrice
        });

        return paginatedResult;
    }
}