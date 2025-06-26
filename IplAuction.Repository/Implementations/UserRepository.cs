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

    public async Task<PaginatedResult<UserResponseViewModel>> GetFilteredUsersAsync(UserFilterParam filterParams)
    {
        var query = _context.Users.Where(u => u.IsDeleted != true).AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(filterParams.Search))
        {
            string search = filterParams.Search.ToLower();

            query = query.Where(u =>
                u.FirstName.ToLower().Contains(search)||
                u.Email.ToLower().Contains(search)
                || (u.LastName != null && u.LastName.ToLower().Contains(search)));
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
        var allowedSorts = new[] { "Name", "DateOfBirth", "Email", "Role" };
        var sortBy = allowedSorts.Contains(filterParams.SortBy) ? filterParams.SortBy : "FirstName";
        var sortDirection = filterParams.SortDirection?.ToLower() == "asc" ? "asc" : "desc";
        if (sortBy == "Name")
        {
            if (sortDirection == "asc")
            {
                query = query.OrderBy(u => u.FirstName).ThenBy(u => u.LastName);
            }
            else
            {
                query = query.OrderByDescending(u => u.FirstName).ThenByDescending(u => u.LastName);
            }
        }
        else
        {
            query = query.OrderBy($"{sortBy} {sortDirection}");
        }


        PaginationParams paginationParams = new()
        {
            PageNumber = filterParams.PageNumber,
            PageSize = filterParams.PageSize
        };

        // Pagination

        PaginatedResult<UserResponseViewModel> paginatedResult = await query.ToPaginatedListAsync(paginationParams, u => new UserResponseViewModel(u));

        return paginatedResult;
    }
}
