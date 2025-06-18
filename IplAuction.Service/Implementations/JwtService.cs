using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IplAuction.Entities;
using IplAuction.Entities.Configurations;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Service.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IplAuction.Service.Implementations;

public class JwtService(IOptions<JwtSettings> jwtSettings) : IJwtService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string GenerateAccessToken(User user)
    {
        var authClaims = new List<Claim>
        {
            new(JwtClaims.Id, user.Id.ToString()),
            new(JwtClaims.Email,user.Email),
            new(JwtClaims.Role, user.Role.ToString())
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken(int day)
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        var token = Convert.ToBase64String(randomBytes);

        return new RefreshToken
        {
            Token = token,
            Expires = DateTime.UtcNow.AddDays(day)
        };
    }

    public UserInfoViewModel DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwtToken = handler.ReadJwtToken(token);

        var email = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtClaims.Email)?.Value;

        var role = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtClaims.Role)?.Value;

        var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtClaims.Id)?.Value;

        return new UserInfoViewModel
        {
            Email = email,
            Role = role,
            Id = userId
        };
    }

    public bool IsTokenExpired(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        if (!tokenHandler.CanReadToken(token))
            return true; // Invalid token, consider it expired

        var jwtToken = tokenHandler.ReadToken(token);

        if (jwtToken == null)
            return true;

        return jwtToken.ValidTo < DateTime.UtcNow;
    }

    public string GeneratePasswordResetToken(string email, int expirationMinute = 10)
    {
        var authClaims = new List<Claim>
        {
            new(JwtClaims.Email,email),
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(expirationMinute),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
