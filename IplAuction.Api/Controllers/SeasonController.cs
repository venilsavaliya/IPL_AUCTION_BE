using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Season;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeasonController(ISeasonService seasonService) : ControllerBase
{
    private readonly ISeasonService _seasonService = seasonService;

    // GET: api/season
    [HttpGet]
    public async Task<IActionResult> GetAllSeasons()
    {
        List<SeasonResponseModel> seasons = await _seasonService.GetAllSeasons();

        var response = ApiResponseBuilder.With<List<SeasonResponseModel>>().StatusCode(200).SetData(seasons).Build();

        return Ok(response);
    }

    // GET: api/season/:id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSeasonById(int id)
    {
        SeasonResponseModel season = await _seasonService.GetSeasonById(id);

        var response = ApiResponseBuilder.With<SeasonResponseModel>().StatusCode(200).SetData(season).Build();

        return Ok(response);
    }

    // POST: api/season
    [HttpPost]
    public async Task<IActionResult> CreateSeason([FromBody] SeasonRequestModel request)
    {
        await _seasonService.AddSeason(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    // PUT: api/season
    [HttpPut]
    public async Task<IActionResult> UpdateSeason([FromBody] UpdateSeasonRequest request)
    {
        await _seasonService.UpdateSeason(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }
}       