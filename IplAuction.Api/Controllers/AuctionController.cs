using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Entities.ViewModels.AuctionPlayer;
using IplAuction.Entities.ViewModels.Player;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Entities.ViewModels.UserTeam;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuctionController(IAuctionService auctionService, IPlayerService playerService) : ControllerBase
{
    private readonly IAuctionService _auctionService = auctionService;

    private readonly IPlayerService _playerService = playerService;

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuctionById(int id)
    {
        AuctionResponseModel auction = await _auctionService.GetAuctionByIdAsync(id);

        var response = ApiResponseBuilder.With<AuctionResponseModel>().SetData(auction).Build();

        return Ok(response);
    }

    [HttpGet("teams/{id}")]
    public async Task<IActionResult> GetAllAuctionTeams(int id)
    {
        List<UserResponseViewModel> teams = await _auctionService.GetAllTeamsOfAuction(id);

        var response = ApiResponseBuilder.With<List<UserResponseViewModel>>().SetData(teams).Build();

        return Ok(response);
    }

    [HttpPost("player/marksold")]
    public async Task<IActionResult> MarkPlayerSold([FromBody] AddUserTeamRequestModel request)
    {
        await _auctionService.MarkPlayerSold(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpPost]
    // [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> CreateAuction([FromBody] AddAuctionRequestModel request)
    {
        await _auctionService.AddAuctionAsync(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAuction([FromBody] UpdateAuctionRequestModel request)
    {
        await _auctionService.UpdateAuctionAsync(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpPost("filter")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuction([FromBody] AuctionFilterParam filterParams)
    {
        var result = await _auctionService.GetAuctionsAsync(filterParams);
        var response = ApiResponseBuilder.With<PaginatedResult<AuctionResponseModel>>().SetData(result).Build();
        return Ok(response);
    }

    [HttpPost("join/{id}")]
    public async Task<IActionResult> JoinAuction(int id)
    {
        var isJoined = await _auctionService.JoinAuctionAsync(id);

        if (isJoined)
        {
            var response = ApiResponseBuilder.Create(200);
            return Ok(response);
        }
        else
        {
            var response = ApiResponseBuilder.Create(400, Messages.RegistrationOver);
            return BadRequest(response);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuction(int id)
    {
        await _auctionService.DeleteAuctionAsync(id);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpGet("next-player/{auctionId}")]
    public async Task<IActionResult> GetUnAuctionedPlayer(int auctionId)
    {
        PlayerResponseModel player = await _playerService.GetRadomUnAuctionedPlayer(auctionId);

        var response = ApiResponseBuilder.With<PlayerResponseModel>().StatusCode(200).SetData(player).Build();

        return Ok(response);
    }

    [HttpGet("currentPlayer/{auctionId}")]
    public async Task<IActionResult> GetCurrentAuctionPlayer(int auctionId)
    {
        PlayerResponseModel player = await _auctionService.GetCurrentAuctionPlayer(auctionId);

        var response = ApiResponseBuilder.With<PlayerResponseModel>().StatusCode(200).SetData(player).Build();

        return Ok(response);
    }

    [HttpPost("setcurrentplayer")]
    public async Task<IActionResult> SetCurrentAuctionPlayer(AuctionPlayerRequest request)
    {
        await _auctionService.SetCurrentPlayerForAuction(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpPost("participated/all")]
    public async Task<IActionResult> GetAllJoinedAuctionsOfUser(UserAuctionFilterParam filterParams)
    {
        PaginatedResult<UserAuctionResponseModel> userAuctions = await _auctionService.GetAllJoinedAuctionsOfUser(filterParams);

        var response = ApiResponseBuilder.With<PaginatedResult<UserAuctionResponseModel>>().SetData(userAuctions).Build();

        return Ok(response);
    }

    [HttpGet("seasonId/{auctionId}")]
    public async Task<IActionResult> GetSeasonIdFromAuctionId(int auctionId)
    {
        int seasonId = await _auctionService.GetSeasonIdFromAuctionId(auctionId);

        return Ok(seasonId);
    }

    [HttpPost("mark-completed/{auctionId}")]
    public async Task<IActionResult> MarkAuctionAsCompleted(int auctionId)
    {
        await _auctionService.MarkAuctionAsCompleted(auctionId);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }
}
