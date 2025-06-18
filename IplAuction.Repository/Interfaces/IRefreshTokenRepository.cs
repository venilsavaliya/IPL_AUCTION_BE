using IplAuction.Entities.Models;

namespace IplAuction.Repository.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetToken(string token);

    Task<bool> Delete(string token);
}
