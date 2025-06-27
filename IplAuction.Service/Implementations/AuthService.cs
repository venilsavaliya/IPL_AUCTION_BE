using IplAuction.Entities;
using IplAuction.Entities.Configurations;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.DTOs.Auth;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Helper;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IplAuction.Service.Implementations;

public class AuthService(IJwtService jwtService,
    IHttpContextAccessor httpContextAccessor, IOptions<JwtSettings> jwtsettings, IEmailService emailservice, IUserService userService, IRefreshTokenService refreshTokenService) : IAuthService
{
    private readonly IJwtService _jwtService = jwtService;

    private readonly IEmailService _emailservice = emailservice;

    private readonly IUserService _userService = userService;

    private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;

    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    private readonly JwtSettings _jwtSettings = jwtsettings.Value;

    public async Task<JwtTokensResponseModel> LoginAsync(LoginRequest loginRequest)
    {
        User? user = await _userService.GetUserByEmailAsync(loginRequest.Email);

        if (user == null || !Password.VerifyPassword(loginRequest.Password, user.PasswordHash))
        {
            throw new BadRequestException(Messages.InvalidCredentials);
        }

        var accessToken = _jwtService.GenerateAccessToken(user);

        var refreshToken = _jwtService.GenerateRefreshToken(loginRequest.RememberMe ? _jwtSettings.RefreshTokenExpirationDays : 1);

        await _userService.AddRefreshTokenAsync(user, refreshToken);

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
        await _userService.CreateUserAsync(request);
    }

    public async Task RefreshTokenAsync()
    {

        var cookieRefreshToken = (_httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"]) ?? throw new UnauthorizedAccessException(Messages.UnAuthorize);

        var storedToken = await _refreshTokenService.GetToken(cookieRefreshToken);

        var user = storedToken.User;

        var newAccessToken = _jwtService.GenerateAccessToken(user);

        var newRefreshToken = _jwtService.GenerateRefreshToken(1);

        _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", newRefreshToken.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = newRefreshToken.Expires
        });

        _httpContextAccessor.HttpContext?.Response.Cookies.Append("accessToken", newAccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes)
        });

        await _userService.AddRefreshTokenAsync(user, newRefreshToken);
    }

    public async Task Logout()
    {
        string? refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("accessToken");
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refreshToken");

        if (refreshToken != null)
        {
            await _refreshTokenService.DeleteToken(refreshToken);
        }
    }

    public async Task ResetPassword(ResetPasswordRequest request)
    {
        bool isTokenExpired = _jwtService.IsTokenExpired(request.Token);

        if (isTokenExpired)
        {
            throw new UnauthorizedAccessException();
        }
        await _userService.UpdatePasswordAsync(request.Email, request.Password);
    }

    public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
    {
        User user = await _userService.GetUserByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(User));

        var jwtToken = _jwtService.GeneratePasswordResetToken(request.Email, 60 * 24);

        var url = "<a>http://localhost:5173/resetPassword?token=" + jwtToken + "</a>";

        bool isMailSent = _emailservice.SendEmail(request.Email, "Reset Password", url);

        return isMailSent;
    }

    public async Task<UserInfoViewModel> GetCurrentUser()
    {
        var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];

        UserInfoViewModel user = new();

        if (accessToken != null)
        {
            user = _jwtService.DecodeToken(accessToken);
        }

        var user1 = await _userService.GetByIdAsync(int.Parse(user.Id??"0"));
        user.ImageUrl = user1?.Image;
        return user;
    }
}
