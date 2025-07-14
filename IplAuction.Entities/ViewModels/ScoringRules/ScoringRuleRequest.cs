using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.ScoringRules;

public class ScoringRuleRequest
{
    public CricketEventType EventType { get; set; }

    public int Points { get; set; }
}
