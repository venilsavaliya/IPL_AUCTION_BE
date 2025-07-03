using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.User;

namespace IplAuction.Repository.Interfaces;

public interface IAuctionParticipantRepository : IGenericRepository<AuctionParticipants>
{
    Task<List<UserResponseViewModel>> GetAllParticipantsByAuctionIdAsync(int auctionId);
}
