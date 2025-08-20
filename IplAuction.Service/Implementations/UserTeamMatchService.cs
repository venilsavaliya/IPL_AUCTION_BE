using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class UserTeamMatchService(IUserTeamMatchRepository userTeamMatchRepository) : IUserTeamMatchService
{

    private readonly IUserTeamMatchRepository _userTeamMatchRepository = userTeamMatchRepository;

    public async Task AddUserTeamMatches(List<UserTeamMatch> userTeamMatches)
    {
        await _userTeamMatchRepository.AddRangeAsync(userTeamMatches);

        await _userTeamMatchRepository.SaveChangesAsync();
    }
}
