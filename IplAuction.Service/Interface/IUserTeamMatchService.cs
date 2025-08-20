using IplAuction.Entities.Models;

namespace IplAuction.Service.Interface;

public interface IUserTeamMatchService
{
    Task AddUserTeamMatches(List<UserTeamMatch> userTeamMatches);
}
