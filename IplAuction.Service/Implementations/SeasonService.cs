using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Season;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class SeasonService(ISeasonRepository seasonRepository) : ISeasonService
{
    private readonly ISeasonRepository _seasonRepository = seasonRepository;

    public async Task<SeasonResponseModel> GetSeasonById(int id)
    {
        SeasonResponseModel season = await _seasonRepository.GetWithFilterAsync(s => s.Id == id, s => new SeasonResponseModel(s)) ?? throw new NotFoundException(nameof(Season));

        return season;
    }

    public async Task<List<SeasonResponseModel>> GetAllSeasons()
    {
        List<SeasonResponseModel> seasons = await _seasonRepository.GetAllAsync(s => new SeasonResponseModel(s));
        seasons = seasons.OrderByDescending(s => int.Parse(s.Name)).ToList();
        return seasons;
    }

    public async Task AddSeason(SeasonRequestModel request)
    {
        Season newSeason = new()
        {
            Name = request.Name
        };

        await _seasonRepository.AddAsync(newSeason);

        await _seasonRepository.SaveChangesAsync();
    }

    public async Task UpdateSeason(UpdateSeasonRequest request)
    {
        Season existingSeason = await _seasonRepository.GetWithFilterAsync(s => s.Id == request.Id) ?? throw new NotFoundException(nameof(Season));

        existingSeason.Name = request.Name;

        await _seasonRepository.SaveChangesAsync();
    }

    public async Task StartSeason(int id)
    {
        Season existingSeason = await _seasonRepository.GetWithFilterAsync(s => s.Id == id) ?? throw new NotFoundException(nameof(Season));

        if (!existingSeason.IsSeasonStarted)
        {
            existingSeason.IsSeasonStarted = true;

            await _seasonRepository.SaveChangesAsync();
        }
    }

    public async Task<SeasonStatusResponseModel> GetSeasonStartStatus(int id)
    {
        Season existingSeason = await _seasonRepository.GetWithFilterAsync(s => s.Id == id) ?? throw new NotFoundException(nameof(Season));

        SeasonStatusResponseModel response = new(existingSeason);

        return response;
    }
}
