namespace IplAuction.Entities.ViewModels.Season;

public class SeasonStatusResponseModel
{
    public SeasonStatusResponseModel(){}
    public SeasonStatusResponseModel(Models.Season season)
    {
        Id = season.Id;
        IsSeasonStarted = season.IsSeasonStarted;
    }
    public int Id { get; set; }
    public bool IsSeasonStarted { get; set; }
}
