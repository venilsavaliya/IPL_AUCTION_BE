using IplAuction.Entities.DTOs;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<List<UserResponseViewModel>> GetAllAsync()
    {
        List<UserResponseViewModel> users = await _userRepository.GetAllWithFilterAsync(u => u.IsDeleted == false, u => new UserResponseViewModel
        {
            Id = u.Id,
            Email = u.Email,
            Role = u.Role.ToString(),
            Username = u.Username
        });

        return users;
    }

    public async Task<UserResponseViewModel> GetByIdAsync(int id)
    {
        UserResponseViewModel user = await _userRepository.GetWithFilterAsync(u => u.Id == id && u.IsDeleted == false, u => new UserResponseViewModel
        {
            Id = u.Id,
            Email = u.Email,
            Role = u.Role.ToString(),
            Username = u.Username
        }) ?? throw new NotFoundException(nameof(User));

        return user;
    }

    public async Task UpdateUserAsync(UpdateUserRequestModel model)
    {
        User user = await _userRepository.FindAsync(model.Id) ?? throw new NotFoundException(nameof(User));

        user.Username = model.Username;
        user.Role = model.Role;
        user.Email = model.Email;

        await _userRepository.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        User user = await _userRepository.FindAsync(id) ?? throw new NotFoundException(nameof(User));

        user.IsDeleted = true;

        await _userRepository.SaveChangesAsync();
    }

    public async Task<PaginatedResult<UserResponseViewModel>> GetUsersAsync(UserFilterParam filterParams)
    {
        return await _userRepository.GetFilteredUsersAsync(filterParams);
    }
}
