using IplAuction.Entities.Enums;
using Microsoft.AspNetCore.Http;

namespace IplAuction.Entities.ViewModels.Player;

public class AddPlayerRequest
{
    public string Name { get; set; } = null!;
    public PlayerSkill Skill { get; set; }
    public int TeamId { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public decimal BasePrice { get; set; }
    public IFormFile? Image { get; set; }
}
