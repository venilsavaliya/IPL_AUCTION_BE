using IplAuction.Entities.Models;

namespace IplAuction.Service.Interface;

public interface IRefreshTokenService
{
    Task<RefreshToken> GetToken(string token);

    Task<bool> DeleteToken(string token);
}
