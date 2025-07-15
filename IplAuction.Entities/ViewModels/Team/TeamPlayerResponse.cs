namespace IplAuction.Entities.ViewModels.Team;

public class TeamPlayerResponse
{
    public TeamPlayerResponse() { }
    public TeamPlayerResponse(Models.Player p)
    {
        PlayerId = p.Id;
        Name = p.Name;
        ImageUrl = p.Image;
    }
    public int PlayerId { get; set; }

    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }
}
