using IplAuction.Entities.ViewModels.Auction;

namespace IplAuction.Entities.ViewModels.User;

public class UserAuctionFilterParam : AuctionFilterParam
{
    public int UserId { get; set; }
}
