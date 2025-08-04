using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Entities.ViewModels.User;

namespace IplAuction.Repository.Interfaces;

public interface IAuctionParticipantRepository : IGenericRepository<AuctionParticipants>
{
    Task<List<UserResponseViewModel>> GetAllParticipantsByAuctionIdAsync(int auctionId);

    Task<List<AuctionParticipantResponseModel>> GetAuctionParticipants(int auctionId);

    Task<AuctionParticipantResponseModel> GetAuctionParticipant(AuctionParticipantRequestModel request);

    Task<List<AuctionTeamResponseModel>> GetAllJoinedTeams(int auctionId);

    Task<List<AuctionParticipantDetail>> GetAuctionParticipantsDetailList(AuctionParticipantDetailRequestModel request);
}
