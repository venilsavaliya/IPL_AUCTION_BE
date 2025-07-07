using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.UserTeam;

public class UserTeamResponseModel
{
    public int PlayerId { get; set; }

    public int Price { get; set; }

    public string Name { get; set; } = null!;

    public PlayerSkill Skill { get; set; }

    public string Image { get; set; } = null!;
}
