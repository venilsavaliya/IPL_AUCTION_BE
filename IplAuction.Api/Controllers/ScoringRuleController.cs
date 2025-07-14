using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.ScoringRules;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ScoringRuleController(IScoringRulesService scoringRulesService) : ControllerBase
{
    private readonly IScoringRulesService _scoringRulesService = scoringRulesService;

    [HttpGet("All")]
    public async Task<IActionResult> GetAllRules()
    {
        List<ScoringRuleResponse> result = await _scoringRulesService.GetAllScoringRules();

        var response = ApiResponseBuilder.With<List<ScoringRuleResponse>>().SetData(result).Build();

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRules(List<UpdateScoringRuleRequest> request)
    {
        await _scoringRulesService.UpdateScoringRules(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }
}
