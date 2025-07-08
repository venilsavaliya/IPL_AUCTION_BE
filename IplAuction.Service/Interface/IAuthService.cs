using IplAuction.Entities.DTOs;
using IplAuction.Entities.DTOs.Auth;
using IplAuction.Entities.ViewModels;
using IplAuction.Entities.ViewModels.Auth;
using IplAuction.Entities.ViewModels.User;

namespace IplAuction.Service.Interface;

public interface IAuthService
{
    Task<JwtTokensResponseModel> LoginAsync(LoginRequest loginRequest);

    Task RegisterAsync(AddUserRequestModel request);

    Task Logout();

    Task<RefreshTokenResponse> RefreshTokenAsync();

    Task<UserInfoViewModel> GetCurrentUser();

    Task ResetPassword(ResetPasswordRequest request);
    
    Task<bool> ForgotPassword(ForgotPasswordRequest request);
}
