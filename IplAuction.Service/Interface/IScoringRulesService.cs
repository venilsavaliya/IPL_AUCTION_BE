using IplAuction.Entities.ViewModels.ScoringRules;

namespace IplAuction.Service.Interface;

public interface IScoringRulesService
{
    Task<List<ScoringRuleResponse>> GetAllScoringRules();

    Task UpdateScoringRules(List<UpdateScoringRuleRequest> request);
}
