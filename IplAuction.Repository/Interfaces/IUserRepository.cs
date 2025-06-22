using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.User;

namespace IplAuction.Repository.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task AddRefreshTokenAsync(User user, RefreshToken refreshToken);
    Task<PaginatedResult<UserResponseViewModel>> GetFilteredUsersAsync(UserFilterParam filterParams);

}
