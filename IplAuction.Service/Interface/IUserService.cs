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

    Task<PaginatedResult<User>> GetPaginated(PaginationParams paginationParams);
}
