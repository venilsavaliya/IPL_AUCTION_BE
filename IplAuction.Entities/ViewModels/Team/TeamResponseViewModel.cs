namespace IplAuction.Entities.ViewModels.Team;

public class TeamResponseViewModel
{
    public TeamResponseViewModel() { }

    public TeamResponseViewModel(Models.Team team)
    {
        Id = team.Id;
        Name = team.Name;
        Image = team.Image;
    }
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Image { get; set; }
}
