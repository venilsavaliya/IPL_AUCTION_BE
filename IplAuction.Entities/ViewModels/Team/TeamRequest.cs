using Microsoft.AspNetCore.Http;

namespace IplAuction.Entities.DTOs.Team;

public class TeamRequest
{
    public string Name { get; set; } = null!;

    public IFormFile? Image { get; set; }
}
