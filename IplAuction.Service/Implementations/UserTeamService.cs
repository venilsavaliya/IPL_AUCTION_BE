using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Entities.ViewModels.Notification;
using IplAuction.Entities.ViewModels.Player;
using IplAuction.Entities.ViewModels.UserTeam;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class UserTeamService(IUserTeamRepository userTeamRepository, IAuctionParticipantService auctionParticipantService, INotificationService notificationService, IPlayerService playerService, ICurrentUserService currentUserService) : IUserTeamService
{
    private readonly IUserTeamRepository _userTeamRepository = userTeamRepository;
    // private readonly IAuctionService _auctionService = auctionService;
    private readonly IAuctionParticipantService _auctionParticipantService = auctionParticipantService;

    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly INotificationService _notificationService = notificationService;

    private readonly IPlayerService _playerService = playerService;

    public async Task AddUserTeam(AddUserTeamRequestModel request)
    {
        UserTeam userTeam = new()
        {
            UserId = request.UserId,
            AuctionId = request.AuctionId,
            PlayerId = request.PlayerId,
            Price = request.Price
        };

        await _userTeamRepository.AddAsync(userTeam);

        await _userTeamRepository.SaveChangesAsync();
    }

    public async Task<List<UserTeamResponseModel>> GetUserTeams(UserTeamRequestModel request)
    {
        if (request.UserId == 0)
        {
            int userId = _currentUserService.UserId;

            request.UserId = userId;
        }
    
        return await _userTeamRepository.GetUserTeams(request);
    }
}
