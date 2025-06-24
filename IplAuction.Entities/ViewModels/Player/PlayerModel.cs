using IplAuction.Entities.Enums;
using IplAuction.Entities.Helper;

namespace IplAuction.Entities.ViewModels.Player;

public class PlayerModel
{
    public int PlayerId { get; set; }
    public string Name { get; set; } = null!;
    public PlayerSkill Skill { get; set; }
    public bool IsActive { get; set; }
    public string Country { get; set; } = null!;
    public decimal BasePrice { get; set; }
    public string? ImageUrl { get; set; }
}

public class PlayerResponseModel : PlayerModel
{
    public PlayerResponseModel() { }
    public PlayerResponseModel(Models.Player p)
    {
        PlayerId = p.Id;
        Name = p.Name;
        ImageUrl = p.Image;
        BasePrice = p.BasePrice;
        Age = CalculateAge.CalculateAgeFromDbo(p.DateOfBirth);
        Country = p.Country;
        IsActive = p.IsActive;
        Skill = p.Skill;
    }
    public string TeamName { get; set; } = null!;
    public int Age { get; set; }
}

public class PlayerResponseDetailModel : PlayerModel
{
    public int TeamId { get; set; }
    public DateOnly DateOfBirth { get; set; }
}