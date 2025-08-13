using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.BallEvent;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BallEventController(IBallEventService balleventService) : ControllerBase
{
    private readonly IBallEventService _balleventService = balleventService;


    [HttpPost]
    public async Task<IActionResult> AddBall([FromBody] AddBallEventRequest request)
    {
        await _balleventService.AddBall(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpGet("outplayerslist/{matchId}")]
    public async Task<IActionResult> GetOutPlayersListByMatchId(int matchId)
    {
        var result = await _balleventService.GetOutPlayersListByMatchId(matchId);

        var response = ApiResponseBuilder.With<List<int>>().StatusCode(200).SetData(result).Build();
        
        return Ok(response);
    }
}
