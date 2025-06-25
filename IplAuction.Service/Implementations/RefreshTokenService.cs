using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class RefreshTokenService(IRefreshTokenRepository refreshTokenRepo) : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepo = refreshTokenRepo;

    public async Task<RefreshToken> GetToken(string token)
    {
        var storedToken = await _refreshTokenRepo.GetToken(token);

        if (storedToken == null || !storedToken.IsActive)
        {
            throw new UnauthorizedAccessException();
        }

        return storedToken;
    }

    public async Task<bool> DeleteToken(string token)
    {
        RefreshToken? refreshToken = await _refreshTokenRepo.GetToken(token);

        if (refreshToken != null)
        {
            await _refreshTokenRepo.Delete(token);
        }

        return true;
    }
}
