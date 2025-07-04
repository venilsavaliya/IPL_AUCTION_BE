using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.AuctionPlayer;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionPlayerController(IAuctionPlayerService auctionPlayerService) : ControllerBase
{
    private readonly IAuctionPlayerService _auctionPlayerService = auctionPlayerService;

    [HttpPost]
    public async Task<IActionResult> AddAuctionPlayer([FromBody] AddAuctionPlayerRequest request)
    {
        await _auctionPlayerService.AddAuctionPlayer(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

}
