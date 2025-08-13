using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.User;

namespace IplAuction.Service.Interface;

public interface IJwtService
{
    string GenerateAccessToken(User user);

    RefreshToken GenerateRefreshToken(int day);

    UserInfoViewModel DecodeToken(string token);

    string GeneratePasswordResetToken(string email, int expirationMinute = 10);

    bool IsTokenExpired(string token);
}
