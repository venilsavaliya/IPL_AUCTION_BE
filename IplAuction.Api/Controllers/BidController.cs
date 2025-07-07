using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Bid;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BidController(IBidService bidService, IBidQueueService bidQueueService) : ControllerBase
{
    private readonly IBidService _bidService = bidService;
    private readonly IBidQueueService _bidQueueService = bidQueueService;

    [HttpPost("place")]
    public async Task<IActionResult> PlaceBid([FromBody] PlaceBidRequestModel request)
    {
        // _bidQueueService.Enqueue(request);

        // return StatusCode(200, Messages.BidWillProcess);
        await _bidService.PlaceOfflineBid(request);

        var response = ApiResponseBuilder.Create(200);

        return Ok(response);
    }

    [HttpPost("latest")]
    public async Task<IActionResult> GetLatestBid([FromBody] LatestBidRequestModel request)
    {
        var bid = await _bidService.GetLatestBidByAuctionId(request);

        var response = ApiResponseBuilder.With<BidResponseModel>().SetData(bid).Build();

        return Ok(response);
    }
}
