using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.Player;

public class PlayerResponseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public PlayerSkill Skill { get; set; } 
    public int TeamId { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public decimal BasePrice { get; set; }
    public string? Image { get; set; }
}
