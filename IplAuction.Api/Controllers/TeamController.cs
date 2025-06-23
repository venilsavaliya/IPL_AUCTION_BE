using IplAuction.Entities.DTOs;
using IplAuction.Entities.DTOs.Team;
using IplAuction.Entities.ViewModels.Team;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController(ITeamService teamService) : ControllerBase
{
    private readonly ITeamService _teamService = teamService;

    // GET: api/team/All
    [HttpGet("All")]
    public async Task<IActionResult> GetAllTeams()
    {
        List<TeamResponseViewModel> teams = await _teamService.GetAllTeamAsync();

        var response = ApiResponseBuilder.With<List<TeamResponseViewModel>>().StatusCode(200).SetData(teams).Build();

        return Ok(response);
    }

    // GET: api/team/:id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamById(int id)
    {
        TeamResponseViewModel team = await _teamService.GetTeamByIdAsync(id);

        var response = ApiResponseBuilder.With<TeamResponseViewModel>().StatusCode(200).SetData(team).Build();

        return Ok(response);
    }

    // POST: api/team
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateTeam([FromForm] TeamRequest team)
    {
        await _teamService.AddTeamAsync(team);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    // PUT: api/team
    [HttpPut]
    public async Task<IActionResult> UpdateTeam([FromBody] UpdateTeamRequest team)
    {
        await _teamService.UpdateTeamAsync(team);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    // DELETE: api/team/:id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(int id)
    {
        await _teamService.DeleteTeamAsync(id);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }
}
