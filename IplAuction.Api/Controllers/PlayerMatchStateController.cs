using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.PlayerMatchState;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerMatchStateController : ControllerBase
{
    private readonly IPlayerMatchStateService _playerMatchStateService;

    public PlayerMatchStateController(IPlayerMatchStateService playerMatchStateService)
    {
        _playerMatchStateService = playerMatchStateService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPlayerMatchState(AddPlayerMatchStateRequest request)
    {
        await _playerMatchStateService.AddPlayerMatchState(request);
        var response = ApiResponseBuilder.Create(200);
        return Ok(response);
    }

    [HttpPost("get")]
    public async Task<IActionResult> GetPlayerMatchStates(PlayerMatchStateRequestParams request)
    {
        List<PlayerMatchStateResponse> data = await _playerMatchStateService.GetPlayerMatchStates(request);
        var response = ApiResponseBuilder.With<List<PlayerMatchStateResponse>>().SetData(data).Build();
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePlayerMatchState(List<UpdatePlayerMatchStateRequest> request)
    {
        await _playerMatchStateService.UpdatePlayerMatchState(request);
        var response = ApiResponseBuilder.Create(200);
        return Ok(response);
    }
}
