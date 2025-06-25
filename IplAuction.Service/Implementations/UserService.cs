using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Helper;
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

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        User? user = await _userRepository.GetWithFilterAsync(u => u.Email == email);
        return user;
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

    public async Task AddRefreshTokenAsync(User user, RefreshToken refreshToken)
    {
        await _userRepository.AddRefreshTokenAsync(user, refreshToken);
    }

    public async Task UpdatePasswordAsync(string email, string newPassword)
    {
        User user = await GetUserByEmailAsync(email) ?? throw new NotFoundException(nameof(User));
        user.PasswordHash = Password.HashPassword(newPassword);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<User> CreateUserAsync(AddUserRequestModel request)
{
    if (await IsEmailExistsAsync(request.Email))
        throw new BadRequestException(Messages.EmailAlreadyExisted);

    var passwordHash = Password.HashPassword(request.Password);

    var user = new User
    {
        Username = request.Username,
        Email = request.Email,
        PasswordHash = passwordHash,
        Role = UserRole.User
    };

    await _userRepository.AddAsync(user);
    await _userRepository.SaveChangesAsync();

    return user;
}
}
