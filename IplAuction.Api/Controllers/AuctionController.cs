using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Entities.ViewModels.Player;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuctionController(IAuctionService auctionService) : ControllerBase
{
    private readonly IAuctionService _auctionService = auctionService;

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
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
        return Ok(result);
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

    // [HttpGet("/{auctionId}/next-player")]
    // public async Task<IActionResult> GetUnAuctionedPlayer(int auctionId)
    // {
    //     PlayerResponseDetailModel player = await _auctionService.GetRandomUnAuctionedPlayer(auctionId);

    //     var response = ApiResponseBuilder.With<PlayerResponseDetailModel>().StatusCode(200).SetData(player).Build();

    //     return Ok(response);
    // }

    // [HttpPost("/AddPlayer")]
    // public async Task<IActionResult> AddPlayerToAuction([FromBody] ManageAuctionPlayerRequest request)
    // {
    //     await _auctionService.AddPlayerToAuction(request);

    //     var response = ApiResponseBuilder.Create(200);

    //     return Ok(response);
    // }

    // [HttpDelete("/RemovePlayer")]
    // public async Task<IActionResult> RemovePlayerFromAuction([FromBody] ManageAuctionPlayerRequest request)
    // {
    //     await _auctionService.RemovePlayerFromAuction(request);

    //     var response = ApiResponseBuilder.Create(200);

    //     return Ok(response);
    // }
}
