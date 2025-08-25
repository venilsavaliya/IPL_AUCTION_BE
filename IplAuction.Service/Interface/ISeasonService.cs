using IplAuction.Entities.ViewModels.Season;

namespace IplAuction.Service.Interface;

public interface ISeasonService
{
    Task<SeasonResponseModel> GetSeasonById(int id);
    Task<List<SeasonResponseModel>> GetAllSeasons();
    Task AddSeason(SeasonRequestModel request);
    Task UpdateSeason(UpdateSeasonRequest request);
    Task StartSeason(int id);
    Task<SeasonStatusResponseModel> GetSeasonStartStatus(int id);
}
