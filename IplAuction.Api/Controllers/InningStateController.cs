using IplAuction.Entities.ViewModels.InningState;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IplAuction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InningStateController(IInningStateService service) : ControllerBase
{
    private readonly IInningStateService _service = service;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        
        return Ok(result);
    }

    [HttpGet("match/{matchId}")]
    public async Task<IActionResult> GetByMatchId(int matchId)
    {
        var result = await _service.GetByMatchIdAsync(matchId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] InningStateRequestModel state)
    {
        await _service.AddAsync(state);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateInningStateRequest state)
    {
        await _service.UpdateAsync(state);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
} 