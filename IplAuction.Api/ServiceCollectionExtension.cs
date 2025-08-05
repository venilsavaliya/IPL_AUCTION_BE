using IplAuction.Api.SignalR;
using IplAuction.Repository.Implementations;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Implementations;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;

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
        services.AddScoped<IBidService, BidService>();
        services.AddScoped<IBidQueueService, InMemoryBidQueueSevice>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IAuctionRepository, AuctionRepository>();
        services.AddScoped<IAuctionParticipantService, AuctionParticipantService>();
        services.AddScoped<IAuctionPlayerService, AuctionPlayerService>();
        services.AddScoped<IAuctionParticipantRepository, AuctionParticipantRepository>();
        services.AddScoped<IAuctionPlayerRepository, AuctionPlayerRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IBidRepository, BidRepository>();
        services.AddScoped<IAuctionPlayerService, AuctionPlayerService>();
        services.AddScoped<IAuctionPlayerRepository, AuctionPlayerRepository>();
        services.AddScoped<IUserTeamService, UserTeamService>();
        services.AddScoped<IUserTeamRepository, UserTeamRepository>();
        services.AddScoped<IAuctionParticipantRepository, AuctionParticipantRepository>();
        services.AddScoped<IAuctionParticipantService, AuctionParticipantService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IMatchService, MatchService>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IScoringRuleRepository, ScoringRuleRepository>();
        services.AddScoped<IScoringRulesService, ScoringRulesService>();
        services.AddScoped<IBallEventRepository, BallEventRepository>();
        services.AddScoped<IBallEventService, BallEventService>();
        services.AddScoped<IInningStateRepository, InningStateRepository>();
        services.AddScoped<IInningStateService, InningStateService>();
        services.AddScoped<IPlayerMatchStateRepository, PlayerMatchStateRepository>();
        services.AddScoped<IPlayerMatchStateService, PlayerMatchStateService>();
        services.AddScoped<ISeasonRepository, SeasonRepository>();
        services.AddScoped<ISeasonService, SeasonService>();
        services.AddScoped<IMatchPointservice, MatchPointService>();
        services.AddScoped<ICalculatePlayerPointsService, CalculatePlayerPointsService>();
        services.AddScoped<IPlayerImportService, PlayerImportService>();

        services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
        services.AddScoped<IUnitOfWork,UnitOfWork >();

        return services;
    }
}