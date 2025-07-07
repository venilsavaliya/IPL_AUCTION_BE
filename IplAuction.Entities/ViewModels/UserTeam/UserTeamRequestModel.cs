namespace IplAuction.Entities.ViewModels.UserTeam;


public class UserTeamRequestModel
{
    public int UserId { get; set; }

    public int AuctionId { get; set; }
}
public class AddUserTeamRequestModel : UserTeamRequestModel
{
    public int PlayerId { get; set; }

    public int Price { get; set; }
}
