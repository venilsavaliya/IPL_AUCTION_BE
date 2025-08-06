using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionParticipantController(IAuctionParticipantService auctionParticipantService) : ControllerBase
{
    private readonly IAuctionParticipantService _auctionParticipantService = auctionParticipantService;

    [HttpGet("{auctionId}")]
    public async Task<IActionResult> GetAllAuctionParticipants(int auctionId)
    {
        List<AuctionParticipantResponseModel> participants = await _auctionParticipantService.GetAllAuctionParticipantsByAuctionId(auctionId);

        var response = ApiResponseBuilder.With<List<AuctionParticipantResponseModel>>().SetData(participants).Build();

        return Ok(response);
    }

    [HttpPost("fetch")]
    public async Task<IActionResult> GetAuctionParticipant([FromBody] AuctionParticipantRequestModel requestModel)
    {
        AuctionParticipantResponseModel participant = await _auctionParticipantService.GetAuctionParticipant(requestModel);

        var response = ApiResponseBuilder.With<AuctionParticipantResponseModel>().SetData(participant).Build();

        return Ok(response);
    }

    [HttpGet("teams/joined/{auctionId}")]
    public async Task<IActionResult> GetAllJoinedTeams(int auctionId)
    {
        List<AuctionTeamResponseModel> teams = await _auctionParticipantService.GetAllJoinedTeams(auctionId);

        var response = ApiResponseBuilder.With<List<AuctionTeamResponseModel>>().SetData(teams).Build();

        return Ok(response);
    }

    [HttpPost("detail")]
    public async Task<IActionResult> GetAuctionPaticipantsDetail(AuctionParticipantDetailRequestModel request)
    {
        List<AuctionParticipantDetail> users = await _auctionParticipantService.GetAuctionParticipantDetails(request);

        var response = ApiResponseBuilder.With<List<AuctionParticipantDetail>>().SetData(users).Build();

        return Ok(response);
    }

    [HttpPost("Alldetail")]

    public async Task<IActionResult> GetAuctionPaticipantsAllDetail(AuctionParticipantAllDetailRequestModel request)
    {
        AuctionParticipantAllDetail user = await _auctionParticipantService.GetAllDetailOfAuctionParticipant(request);

        var response = ApiResponseBuilder.With<AuctionParticipantAllDetail>().SetData(user).Build();

        return Ok(response);
    }

    [HttpPost("PlayersAndDetail")]
    public async Task<IActionResult> GetAuctionParticipantPlayers(ParticipantPlayerRequestModel request)
    {
        AuctionParticipantPlayerResponseModel result = await _auctionParticipantService.GetParticipantsPlayerListAndDetail(request);

        var response = ApiResponseBuilder.With<AuctionParticipantPlayerResponseModel>().SetData(result).Build();

        return Ok(response);
    }
}
