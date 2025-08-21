using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.UserTeam;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class UserTeamService(IUserTeamRepository userTeamRepository, ICurrentUserService currentUserService) : IUserTeamService
{
    private readonly IUserTeamRepository _userTeamRepository = userTeamRepository;

    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task AddUserTeam(AddUserTeamRequestModel request)
    {
        UserTeam userTeam = new()
        {
            UserId = request.UserId,
            AuctionId = request.AuctionId,
            PlayerId = request.PlayerId,
            Price = request.Price,
            IsReshuffled = request.IsReshuffledPlayer,
            ReshuffledStatus = request.IsReshuffledPlayer
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

    public async Task<List<UserTeamOfMatchResponseModel>> GetUserTeamOfMatch(UserTeamOfMatchRequestModel request)
    {
        return await _userTeamRepository.GetUserTeamOfMatch(request);
    }

    public async Task<List<UserTeam>> GetUserTeamsByPlayerIds(List<int> ids)
    {
        return await _userTeamRepository.GetUserTeamsByPlayerIds(ids);
    }

    public async Task ReshufflePlayers(List<ReshufflePlayerRequest> request)
    {
        await _userTeamRepository.ReshufflePlayers(request);
    }

    public async Task<Dictionary<int,bool>> GetReshuffledPlayerDictionaryByUserId(ReshuffledPlayerOfUserRequestModel request)
    {
        List<UserTeam> userTeams = await _userTeamRepository.GetAllWithFilterAsync(ut => ut.AuctionId == request.AuctionId && ut.UserId == request.UserId && ut.IsReshuffled == true);

        Dictionary<int,bool> reshuffledPlayers = userTeams.Select(ut => new ReshuffledPlayer
        {
            PlayerId = ut.PlayerId,
            IsJoined = ut.ReshuffledStatus
        }).ToDictionary(p=>p.PlayerId,p=>p.IsJoined);

        return reshuffledPlayers;
    }
}
