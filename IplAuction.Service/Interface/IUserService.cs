using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.User;

namespace IplAuction.Service.Interface;

public interface IUserService
{
    Task<List<UserResponseViewModel>> GetAllAsync();

    Task<UserResponseViewModel> GetByIdAsync(int id);

    Task UpdateUserAsync(UpdateUserRequestModel model);

    Task DeleteUserAsync(int id);

    Task<PaginatedResult<UserResponseViewModel>> GetUsersAsync(UserFilterParam filterParams);

    Task<User?> GetUserByEmailAsync(string email);

    Task AddRefreshTokenAsync(User user, RefreshToken refreshToken);

    Task UpdatePasswordAsync(string email, string newPassword);

    Task<User> CreateUserAsync(AddUserRequestModel request);
}
