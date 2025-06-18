using IplAuction.Entities.Models;

namespace IplAuction.Repository.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task AddRefreshTokenAsync(User user, RefreshToken refreshToken);
}
