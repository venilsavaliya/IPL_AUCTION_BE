using IplAuction.Entities.DTOs;
using IplAuction.Entities.DTOs.Team;
using IplAuction.Entities.ViewModels.Player;
using IplAuction.Entities.ViewModels.Team;

namespace IplAuction.Service.Interface;

public interface ITeamService
{
    Task<List<TeamResponseViewModel>> GetAllTeamAsync();

    Task<TeamResponseViewModel> GetTeamByIdAsync(int id);

    Task AddTeamAsync(TeamRequest team);

    Task UpdateTeamAsync(UpdateTeamRequest team);

    Task DeleteTeamAsync(int id);
    Task<PaginatedResult<TeamResponseViewModel>> GetTeamsAsync(TeamFilterParam filterParams);

    Task<List<TeamPlayerResponse>> GetAllPlayersByTeamId(int id);

    Dictionary<string, int> GetTeamNameIdDictionary();
}
