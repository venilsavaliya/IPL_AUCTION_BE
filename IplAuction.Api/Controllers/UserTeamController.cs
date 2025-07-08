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
}
