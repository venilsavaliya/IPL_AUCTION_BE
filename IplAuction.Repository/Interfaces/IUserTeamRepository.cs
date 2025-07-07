using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.UserTeam;

namespace IplAuction.Repository.Interfaces;

public interface IUserTeamRepository : IGenericRepository<UserTeam>
{
    Task<List<UserTeamResponseModel>> GetUserTeams(UserTeamRequestModel request);
}
