using IplAuction.Entities.Enums;
using IplAuction.Entities.Models;

namespace IplAuction.Entities.ViewModels.ScoringRules;

public class ScoringRuleResponse
{
    public ScoringRuleResponse() {}

    public ScoringRuleResponse(ScoringRule s)
    {
        Id = s.Id;
        EventType = s.EventType;
        Points = s.Points;
    }
    public int Id { get; set; }

    public CricketEventType EventType { get; set; }

    public int Points { get; set; }
}
