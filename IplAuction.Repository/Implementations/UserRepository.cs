using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace IplAuction.Repository.Implementations;

public class UserRepository(IplAuctionDbContext context) : GenericRepository<User>(context), IUserRepository
{
    public async Task AddRefreshTokenAsync(User user, RefreshToken refreshToken)
    {
        // clear existing tokens of user
        List<RefreshToken> existingTokens = _context.RefreshTokens.Where(rt => rt.UserId == user.Id).ToList();

        _context.RefreshTokens.RemoveRange(existingTokens);

        user.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
    }

    

    // public async Task<PaginatedResult<User>> GetFilteredAsync(UserFilterModel filter)
    // {
    //     var query = _dbSet.AsQueryable();

    //     // Apply filters
    //     if (!string.IsNullOrWhiteSpace(filter.Search))
    //     {
    //         query = query.Where(u =>
    //             u.Username.Contains(filter.Search) ||
    //             u.Email.Contains(filter.Search));
    //     }

    //     if (!string.IsNullOrEmpty(filter.Role))
    //     {
    //         query = query.Where(u => u.Role == filter.Role);
    //     }

    //     if (filter.IsActive.HasValue)
    //     {
    //         query = query.Where(u => u.IsActive == filter.IsActive.Value);
    //     }

    //     // Apply pagination
    //     return await query.ToPaginatedListAsync(filter.Pagination);
    // }

    public async Task<PaginatedResult<UserResponseViewModel>> GetFilteredUsersAsync(UserFilterParam filterParams)
    {
        var query = _context.Users.AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(filterParams.Search))
        {
            string search = filterParams.Search.ToLower();

            query = query.Where(u =>
                u.Username.ToLower().Contains(search) ||
                u.Email.ToLower().Contains(search));
        }

        // Filtering Role
        if (!string.IsNullOrEmpty(filterParams.Role))
        {
            string role = filterParams.Role.ToLower();

            if (Enum.TryParse<UserRole>(filterParams.Role, true, out var roleEnum))
            {
                query = query.Where(u => u.Role == roleEnum);
            }
        }

        // Sorting
        var allowedSorts = new[] { "Username", "Id", "CreatedAt", "Email" };
        var sortBy = allowedSorts.Contains(filterParams.SortBy) ? filterParams.SortBy : "Id";
        var sortDirection = filterParams.SortDirection?.ToLower() == "asc" ? "asc" : "desc";
        query = query.OrderBy($"{sortBy} {sortDirection}");

        PaginationParams paginationParams = new()
        {
            PageNumber = filterParams.PageNumber,
            PageSize = filterParams.PageSize
        };

        // Pagination

        PaginatedResult<UserResponseViewModel> paginatedResult = await query.ToPaginatedListAsync(paginationParams, u => new UserResponseViewModel
        {
            Id = u.Id,
            Email = u.Email,
            Username = u.Username,
            Role = u.Role.ToString(),
            CreatedAt = u.CreatedAt
        });

        return paginatedResult;
    }
}
