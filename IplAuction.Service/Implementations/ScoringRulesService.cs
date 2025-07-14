using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.ScoringRules;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class ScoringRulesService(IScoringRuleRepository scoringRuleRepository) : IScoringRulesService
{
    private readonly IScoringRuleRepository _scoringRuleRepository = scoringRuleRepository;

    public async Task<List<ScoringRuleResponse>> GetAllScoringRules()
    {
        return await _scoringRuleRepository.GetAllScoringRules();
    }
    public async Task UpdateScoringRules(List<UpdateScoringRuleRequest> request)
    {
        await _scoringRuleRepository.UpdateScoringRules(request);
    }

}
