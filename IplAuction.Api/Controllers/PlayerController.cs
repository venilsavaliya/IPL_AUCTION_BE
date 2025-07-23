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

    [HttpPost("filter")]
    public async Task<IActionResult> GetPlayers([FromBody] PlayerFilterParams filterParams)
    {
        var result = await _playerService.GetPlayersAsync(filterParams);

        var response = ApiResponseBuilder.With<PaginatedResult<PlayerResponseModel>>().StatusCode(200).SetData(result).Build();

        return Ok(response);
    }

    [HttpGet("team/{id}")]
    public async Task<IActionResult> GetPlayers(int id)
    {
        var player = await _playerService.GetPlayerDetailByIdAsync(id);

        var response = ApiResponseBuilder.With<PlayerResponseDetailModel>().SetData(player).Build();

        return Ok(response);
    }

    // GET: api/player/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlayer(int id)
    {
        var player = await _playerService.GetPlayerDetailByIdAsync(id);

        var response = ApiResponseBuilder.With<PlayerResponseDetailModel>().SetData(player).Build();

        return Ok(response);
    }

    // GET: api/player/all/namelist
    [HttpGet("all/namelist")]
    public async Task<IActionResult> GetAllPlayerIdName()
    {
        var result = await _playerService.GetAllPlayerIdNameAsync();
        var response = ApiResponseBuilder.With<List<PlayerIdName>>().StatusCode(200).SetData(result).Build();
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

    [HttpPut("status")]
    public async Task<IActionResult> UpdatePlayerStatus([FromBody] UpdatePlayerStatusRequest request)
    {
        await _playerService.UpdatePlayerStatusAsync(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    // PUT: api/player
    [HttpPut]
    public async Task<IActionResult> UpdatePlayer([FromForm] UpdatePlayerRequest player)
    {
        await _playerService.UpdatePlayerAsync(player);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    // [HttpPost("import-csv")]
    // public async Task<IActionResult> ImportCsv(IFormFile file)
    // {
    //     await _playerService.ImportPlayersFromCsvAsync(file);

    //     return Ok(new { Message = "Import successful" });
    // }
}