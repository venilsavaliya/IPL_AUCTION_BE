using IplAuction.Entities;
using IplAuction.Entities.Configurations;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.DTOs.Auth;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IplAuction.Service.Implementations;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;

    private readonly IEmailService _emailservice;

    private readonly IUserRepository _userRepository;

    private readonly IRefreshTokenRepository _refreshTokenRepository;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly JwtSettings _jwtSettings;

    public AuthService(IUserRepository userRepository, IJwtService jwtService, IRefreshTokenRepository refreshTokenRepository,
        IHttpContextAccessor httpContextAccessor, IOptions<JwtSettings> jwtsettings, IEmailService emailservice)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _refreshTokenRepository = refreshTokenRepository;
        _httpContextAccessor = httpContextAccessor;
        _jwtSettings = jwtsettings.Value;
        _emailservice = emailservice;
    }

    public async Task<JwtTokensResponseModel> LoginAsync(LoginRequest loginRequest)
    {
        User? user = await _userRepository.GetWithFilterAsync(u => u.Email == loginRequest.Email);

        if (user == null || !VerifyPassword(loginRequest.Password, user.PasswordHash))
        {
            throw new BadRequestException(Messages.InvalidCredentials);
        }

        var accessToken = _jwtService.GenerateAccessToken(user);

        var refreshToken = _jwtService.GenerateRefreshToken(loginRequest.RememberMe ? _jwtSettings.RefreshTokenExpirationDays : 1);

        await _userRepository.AddRefreshTokenAsync(user, refreshToken);

        _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = refreshToken.Expires
        });

        _httpContextAccessor.HttpContext?.Response.Cookies.Append("accessToken", accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes)
        });

        return new JwtTokensResponseModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
    }

    public async Task RegisterAsync(AddUserRequestModel request)
    {
        var existingUser = await _userRepository.GetWithFilterAsync(u => u.Email == request.Email);

        if (existingUser != null)
        {
            throw new BadRequestException(Messages.EmailAlreadyExisted);
        }

        string passwordHash = HashPassword(request.Password);

        var user = new User
        {

            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = UserRole.User
        };

        await _userRepository.AddAsync(user);

        await _userRepository.SaveChangesAsync();
    }

    public async Task<JwtTokensResponseModel> RefreshTokenAsync(string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetToken(refreshToken);

        if (storedToken == null || !storedToken.IsActive)
        {
            throw new UnauthorizedAccessException();
        }

        var user = storedToken.User;

        var newAccessToken = _jwtService.GenerateAccessToken(user);

        var newRefreshToken = _jwtService.GenerateRefreshToken(1);

        await _userRepository.AddRefreshTokenAsync(user, newRefreshToken);

        return new JwtTokensResponseModel()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token
        };
    }

    public async Task Logout()
    {
        string? refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("accessToken");
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refreshToken");

        if (refreshToken != null)
        {
            await _refreshTokenRepository.Delete(refreshToken);
        }
    }

    public async Task ResetPassword(ResetPasswordRequest request)
    {
        bool isTokenExpired = _jwtService.IsTokenExpired(request.Token);

        if (isTokenExpired)
        {
            throw new UnauthorizedAccessException();
        }

        User user = await _userRepository.GetWithFilterAsync(u => u.Email == request.Email) ?? throw new NotFoundException(nameof(User));

        user.PasswordHash = HashPassword(request.Password);

        await _userRepository.SaveChangesAsync();
    }

    public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
    {
        User user = await _userRepository.GetWithFilterAsync(u => u.Email == request.Email) ?? throw new NotFoundException(nameof(User));

        var jwtToken = _jwtService.GeneratePasswordResetToken(request.Email, 60 * 24);

        var url = "<a>http://localhost:5173/resetPassword?token=" + jwtToken + "</a>";

        bool isMailSent = _emailservice.SendEmail(request.Email, "Reset Password", url);

        return isMailSent;
    }

    public UserInfoViewModel GetCurrentUser()
    {
        var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];

        UserInfoViewModel user = new();

        if (accessToken != null)
        {
            user = _jwtService.DecodeToken(accessToken);
        }
        return user;
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
