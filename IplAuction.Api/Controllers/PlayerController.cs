using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Player;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController(IPlayerService playerService) : ControllerBase
{
    private readonly IPlayerService _playerService = playerService;

    // GET: api/player/All
    [HttpGet("All")]
    public async Task<IActionResult> GetAllPlayers()
    {
        List<PlayerResponseModel> players = await _playerService.GetAllPlayersAsync();

        var response = ApiResponseBuilder.With<List<PlayerResponseModel>>().StatusCode(200).SetData(players).Build();
        return Ok(response);

    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] PlayerFilterParams filterParams)
    {
        var result = await _playerService.GetPlayersAsync(filterParams);
        return Ok(result);
    }

    // GET: api/player/{id}

    [HttpGet("{id}")]

    public async Task<IActionResult> GetPlayer(int id)
    {
        PlayerResponseModel player = await _playerService.GetPlayerByIdAsync(id);

        var response = ApiResponseBuilder.With<PlayerResponseModel>().StatusCode(200).SetData(player).Build();

        return Ok(response);
    }

    // POST: api/player
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreatePlayer([FromForm] AddPlayerRequest player)
    {
        await _playerService.AddPlayerAsync(player);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    // DELETE: api/player/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        await _playerService.DeletePlayerAsync(id);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    // PUT: api/player/{id}
    [HttpPut]
    public async Task<IActionResult> UpdatePlayer([FromBody] UpdatePlayerRequest player)
    {
        await _playerService.UpdatePlayerAsync(player);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

}