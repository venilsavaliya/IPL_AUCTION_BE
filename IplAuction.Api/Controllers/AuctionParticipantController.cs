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
}
