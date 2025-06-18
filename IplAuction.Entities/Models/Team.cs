namespace IplAuction.Entities.Models;

public class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Image { get; set; }

    public bool IsDeleted { get; set; } = false;

    public ICollection<Player> Players { get; set; } = [];
}
