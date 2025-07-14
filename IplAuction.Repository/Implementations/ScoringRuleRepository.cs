using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.ScoringRules;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IplAuction.Repository.Implementations;

public class ScoringRuleRepository(IplAuctionDbContext context, IUnitOfWork unitOfWork) : GenericRepository<ScoringRule>(context), IScoringRuleRepository
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<List<ScoringRuleResponse>> GetAllScoringRules()
    {
        List<ScoringRuleResponse> result = await _context.ScoringRules.Select(s => new ScoringRuleResponse(s)).ToListAsync();

        return result;
    }

    public async Task UpdateScoringRules(List<UpdateScoringRuleRequest> request)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            
            var existingRules = await _context.ScoringRules.ToListAsync();

            var dbDict = existingRules.ToDictionary(r => r.EventType);

            foreach (var item in request)
            {
                if (dbDict.TryGetValue(item.EventType, out var rule))
                {
                    rule.Points = item.Points;
                    // rule.EventType = item.EventType;
                    // _context.ScoringRules.Update(rule);
                }
            }

            await _context.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
