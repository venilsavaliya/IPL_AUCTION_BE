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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMatchById(int id)
    {
        MatchResponse match = await _matchService.GetMatchById(id);

        var response = ApiResponseBuilder.With<MatchResponse>().SetData(match).Build();

        return Ok(response);
    }

    [HttpGet("{matchId}/live")]
    public async Task<IActionResult> GetLiveMatchStatus(int matchId)
    {
        var status = await _matchService.GetLiveMatchStatus(matchId);
        var response = ApiResponseBuilder.With<LiveMatchStatusResponse>().SetData(status).Build();
        return Ok(response);
    }

    [HttpPost("filter")]
    public async Task<IActionResult> GetFilteredMatches([FromBody] MatchFilterParams matchFilterParams)
    {
        PaginatedResult<MatchResponse> result = await _matchService.GetFilteredMatchAsync(matchFilterParams);

        var response = ApiResponseBuilder.With<PaginatedResult<MatchResponse>>().SetData(result).Build();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddMatch([FromBody] MatchRequest request)
    {
        await _matchService.AddMatch(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMatch([FromBody] UpdateMatchRequest request)
    {
        await _matchService.UpdateMatch(request);

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
