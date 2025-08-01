namespace IplAuction.Entities.ViewModels.Season;

public class SeasonResponseModel
{
    public SeasonResponseModel()
    {

    }
    public SeasonResponseModel(Models.Season s)
    {
        Id = s.Id;
        Name = s.Name;
    }
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
