using System.Security.Claims;
using IplAuction.Entities;
using Microsoft.AspNetCore.SignalR;

namespace IplAuction.Api.SignalR;

public class CustomUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst(JwtClaims.Id)?.Value;
    }
}