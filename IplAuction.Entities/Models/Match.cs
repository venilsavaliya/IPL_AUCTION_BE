using IplAuction.Entities.Enums;

namespace IplAuction.Entities.Models;

public class Match
{
    public int Id { get; set; }

    public int? SeasonId { get; set; }

    public int TeamAId { get; set; }

    public int TeamBId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime StartDate { get; set; }

    public int InningNumber { get; set; } = 0;

    public Team TeamA { get; set; } = null!;
    public Team TeamB { get; set; } = null!;
    public ICollection<BallEvent> BallEvents { get; set; } = [];

    public Season? Season { get; set; }

    public ICollection<InningState> InningStates { get; set; } = [];
    public ICollection<PlayerMatchStates> PlayerMatchStates { get; set; } = [];
}
