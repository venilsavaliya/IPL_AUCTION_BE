using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Repository.Interfaces;

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

}
