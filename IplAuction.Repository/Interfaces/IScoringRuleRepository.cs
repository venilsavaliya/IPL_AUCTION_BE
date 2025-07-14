using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.ScoringRules;

namespace IplAuction.Repository.Interfaces;

public interface IScoringRuleRepository : IGenericRepository<ScoringRule>
{
    Task<List<ScoringRuleResponse>> GetAllScoringRules();

    Task UpdateScoringRules(List<UpdateScoringRuleRequest> request);
}
