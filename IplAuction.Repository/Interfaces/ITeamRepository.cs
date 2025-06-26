using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Team;

namespace IplAuction.Repository.Interfaces;

public interface ITeamRepository : IGenericRepository<Team>
{
    Task<PaginatedResult<TeamResponseViewModel>> GetFilteredTeamsAsync(TeamFilterParam filterParams);
}
