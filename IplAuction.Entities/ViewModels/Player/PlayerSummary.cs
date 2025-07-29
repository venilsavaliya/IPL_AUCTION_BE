using IplAuction.Entities.Enums;
namespace IplAuction.Entities.ViewModels.Player;

public class PlayerSummary
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public PlayerSkill Skill { get; set; }
}
