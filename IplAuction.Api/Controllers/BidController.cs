using IplAuction.Entities;
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
    public IActionResult PlaceBid([FromBody] PlaceBidRequestModel request)
    {
        _bidQueueService.Enqueue(request);

        return StatusCode(200, Messages.BidWillProcess);
    }
}
