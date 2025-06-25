using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Helper;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class UserService(IUserRepository userRepository, IEmailService emailService, IFileStorageService fileStorageService) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly IEmailService _emailService = emailService;

    private readonly IFileStorageService _fileStorageService = fileStorageService;

    public async Task<List<UserResponseViewModel>> GetAllAsync()
    {
        List<UserResponseViewModel> users = await _userRepository.GetAllWithFilterAsync(u => u.IsDeleted == false, u => new UserResponseViewModel(u));

        return users;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        User? user = await _userRepository.GetWithFilterAsync(u => u.Email == email);
        return user;
    }

    public async Task<UserResponseViewModel> GetByIdAsync(int id)
    {
        UserResponseViewModel user = await _userRepository.GetWithFilterAsync(u => u.Id == id && u.IsDeleted == false, u => new UserResponseViewModel(u)) ?? throw new NotFoundException(nameof(User));

        return user;
    }

    public async Task UpdateUserAsync(UpdateUserRequestModel model)
    {
        User user = await _userRepository.FindAsync(model.Id) ?? throw new NotFoundException(nameof(User));

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.MobileNumber = model.MobileNumber;
        user.Email = model.Email;
        user.DateOfBirth = model.DateOfBirth;
        user.Role = model.Role;
        user.Gender = model.Gender;
        if (model.Image != null)
        {
            var imageUrl = await _fileStorageService.UploadFileAsync(model.Image);
            user.Image = imageUrl;
        }
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

    public async Task<User> CreateUserAsync(UserRequestModel request)
    {
        var existingUser = await _userRepository.GetWithFilterAsync(u => u.Email == request.Email);

        if (existingUser != null)
        {
            throw new BadRequestException(Messages.EmailAlreadyExisted);
        }

        var passwordHash = Password.HashPassword(request.Password);

        var user = new User()
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = UserRole.User,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MobileNumber = request.MobileNumber,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender
        };

        // user.Role = request is AddUserRequestModel ? ((request as AddUserRequestModel)?.Role ?? UserRole.User) : UserRole.User;

        if (request is AddUserRequestModel addUserRequest)
        {
            user.Role = addUserRequest.Role;

            bool isMailSent = _emailService.SendEmail(request.Email, Messages.AccountCredential,
                $"Your account has been created with the following credentials:\nEmail: {request.Email}\nPassword: {request.Password}");
        }
        else
        {
            user.Role = UserRole.User;
        }

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return user;
    }
}
