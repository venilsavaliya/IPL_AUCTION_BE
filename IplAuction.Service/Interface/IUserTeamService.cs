using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.UserTeam;

namespace IplAuction.Service.Interface;

public interface IUserTeamService
{
    Task AddUserTeam(AddUserTeamRequestModel request);

    Task<List<UserTeamResponseModel>> GetUserTeams(UserTeamRequestModel request);

    Task<List<UserTeam>> GetUserTeamsByPlayerIds(List<int> ids);

    Task<List<UserTeamOfMatchResponseModel>> GetUserTeamOfMatch(UserTeamOfMatchRequestModel request);
}
