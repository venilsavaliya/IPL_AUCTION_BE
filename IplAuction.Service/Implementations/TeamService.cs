using IplAuction.Entities.DTOs;
using IplAuction.Entities.DTOs.Team;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Team;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class TeamService(ITeamRepository teamRepository, IFileStorageService fileStorage) : ITeamService
{
    private readonly ITeamRepository _teamRepository = teamRepository;

    private readonly IFileStorageService _fileStorage = fileStorage;

    public async Task AddTeamAsync(TeamRequest team)
    {
        string? imageUrl = null;

        if (team.Image != null)
        {
            imageUrl = await _fileStorage.UploadFileAsync(team.Image);
        }
        Team newTeam = new()
        {
            Name = team.Name,
            Image = imageUrl
        };

        await _teamRepository.AddAsync(newTeam);

        await _teamRepository.SaveChangesAsync();
    }

    public async Task<List<TeamResponseViewModel>> GetAllTeamAsync()
    {
        List<TeamResponseViewModel> teams = await _teamRepository.GetAllWithFilterAsync(t => t.IsDeleted == false, t => new TeamResponseViewModel
        {
            Id = t.Id,
            Name = t.Name,
            Image = t.Image
        });

        return teams;
    }


    public async Task<TeamResponseViewModel> GetTeamByIdAsync(int id)
    {
        TeamResponseViewModel team = await _teamRepository.GetWithFilterAsync(t => t.IsDeleted == false && t.Id == id, t => new TeamResponseViewModel
        {
            Id = t.Id,
            Name = t.Name,
            Image = t.Image
        }) ?? throw new NotFoundException(nameof(Team));

        return team;
    }

    public async Task UpdateTeamAsync(UpdateTeamRequest team)
    {
        Team existingTeam = await _teamRepository.FindAsync(team.Id) ?? throw new NotFoundException(nameof(Team));

        string? imageUrl = null;

        if (team.Image != null)
        {
            imageUrl = await _fileStorage.UploadFileAsync(team.Image);
        }

        existingTeam.Name = team.Name;
        existingTeam.Image = imageUrl ?? existingTeam.Image;

        await _teamRepository.SaveChangesAsync();
    }

    public async Task DeleteTeamAsync(int id)
    {
        Team team = await _teamRepository.FindAsync(id) ?? throw new NotFoundException(nameof(Team));

        team.IsDeleted = true;

        await _teamRepository.SaveChangesAsync();
    }

    public async Task<PaginatedResult<TeamResponseViewModel>> GetTeamsAsync(TeamFilterParam filterParams)
    {
        return await _teamRepository.GetFilteredTeamsAsync(filterParams);
    }

    public async Task<List<TeamPlayerResponse>> GetAllPlayersByTeamId(int id)
    {
        return await _teamRepository.GetAllPlayersByTeamId(id);
    }

    public Dictionary<string, int> GetTeamNameIdDictionary()
    {
        return _teamRepository.GetAllTeamNameIdDictionary();
    }
}
