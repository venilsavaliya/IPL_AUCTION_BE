using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.UserTeam;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserTeamController(IUserTeamService userTeamService) : ControllerBase
{
    private readonly IUserTeamService _userTeamService = userTeamService;

    [HttpPost]
    public async Task<IActionResult> AddUserTeam(AddUserTeamRequestModel request)
    {
        await _userTeamService.AddUserTeam(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpPost("getlist")]
    public async Task<IActionResult> GetAllTeamPlayers(UserTeamRequestModel request)
    {
        var teams = await _userTeamService.GetUserTeams(request);

        var response = ApiResponseBuilder.With<List<UserTeamResponseModel>>().SetData(teams).Build();

        return Ok(response);
    }

    [HttpPost("matchTeamPlayers")]
    public async Task<IActionResult> GetAllTeamPlayersOfMatch(UserTeamOfMatchRequestModel request)
    {
        var teams = await _userTeamService.GetUserTeamOfMatch(request);

        var response = ApiResponseBuilder.With<List<UserTeamOfMatchResponseModel>>().SetData(teams).Build();

        return Ok(response);
    }

    [HttpPost("ReshufflePlayers")]
    public async Task<IActionResult> ReshufflePlayers(List<ReshufflePlayerRequest> request)
    {
        await _userTeamService.ReshufflePlayers(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }
}
