using IplAuction.Entities.Enums;

namespace IplAuction.Entities.Models;

public class ScoringRule
{
    public int Id { get; set; }

    public CricketEventType EventType { get; set; }

    public int Points { get; set; }
}
