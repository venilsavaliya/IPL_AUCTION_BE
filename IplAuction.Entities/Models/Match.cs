using IplAuction.Entities.Enums;

namespace IplAuction.Entities.Models;

public class Match
{
    public int Id { get; set; }

    public int TeamAId { get; set; }

    public int TeamBId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime StartDate { get; set; }

    public Team TeamA { get; set; } = null!;
    public Team TeamB { get; set; } = null!;

    public ICollection<BallEvent> BallEvents { get; set; } = [];
}
