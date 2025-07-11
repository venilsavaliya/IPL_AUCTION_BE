using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Match;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchController(IMatchService matchService) : ControllerBase
{
    private readonly IMatchService _matchService = matchService;


    [HttpGet("All")]
    public async Task<IActionResult> GetAllMatches()
    {
        List<MatchResponse> matches = await _matchService.GetAllMatch();

        var response = ApiResponseBuilder.With<List<MatchResponse>>().SetData(matches).Build();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddMatch([FromBody] MatchRequest request)
    {
        await _matchService.AddMatch(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMatch(int id)
    {
        await _matchService.DeleteMatch(id);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }
}
