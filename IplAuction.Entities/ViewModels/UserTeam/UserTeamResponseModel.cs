using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.UserTeam;

public class UserTeamResponseModel : UserTeamOfMatchResponseModel
{
    public int Price { get; set; }
    public PlayerSkill Skill { get; set; }
}

public class UserTeamOfMatchResponseModel
{
    public int PlayerId { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
}
