using System.Security.Claims;
using IplAuction.Entities;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace IplAuction.Service.Implementations;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    private ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User ?? throw new UnauthorizedAccessException();

    public int UserId
    {
        get
        {
            var idClaim = User.FindFirst(JwtClaims.Id)?.Value;

            if (string.IsNullOrEmpty(idClaim))
                throw new UnauthorizedAccessException();

            return int.Parse(idClaim);
        }
    }

    public string? Email => User.FindFirst(JwtClaims.Email)?.Value;    

    public string? Role => User.FindFirst(JwtClaims.Role)?.Value;    
}
