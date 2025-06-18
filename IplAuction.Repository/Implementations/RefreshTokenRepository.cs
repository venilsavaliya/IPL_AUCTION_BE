using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IplAuction.Repository.Implementations;

public class RefreshTokenRepository(IplAuctionDbContext context) :GenericRepository<RefreshToken>(context),IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetToken(string token)
    {
        var storedToken = await _context.RefreshTokens
           .Include(rt => rt.User)
           .FirstOrDefaultAsync(rt => rt.Token == token);

        return storedToken;
    }

    public async Task<bool> Delete(string token)
    {
        RefreshToken? refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken != null)
        {
            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();
        }

        return true;
    }
}
