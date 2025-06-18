using IplAuction.Entities.Models;
using IplAuction.Repository.Implementations;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Implementations;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace IplAuction.Api;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAuctionService, AuctionService>();
        services.AddScoped<IBidService,BidService>();
        services.AddScoped<IBidQueueService,InMemoryBidQueueSevice>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IAuctionRepository, AuctionRepository>();
        
        return services;
    }
}