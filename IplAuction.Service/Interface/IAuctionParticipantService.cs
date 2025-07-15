using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Entities.ViewModels.User;

namespace IplAuction.Service.Interface;

public interface IAuctionParticipantService
{
    Task AddParticipantAsync(int auctionId, int userId);

    Task AddParticipantsAsync(List<AuctionParticipants> auctionParticipants);

    Task RemoveParticipantsAsync(List<AuctionParticipants> auctionParticipants);
    Task<List<AuctionParticipants>> GetParticipantsByUserIdsAsync(int auctionid, List<int> auctionParticipants);
    Task<List<AuctionParticipants>> GetParticipantsByAuctionIdAsync(int auctionid);

    Task<List<UserResponseViewModel>> GetAuctionParticipantsByAuctionId(int auctionid);

    Task<List<AuctionParticipantResponseModel>> GetAllAuctionParticipantsByAuctionId(int auctionid);

    Task<AuctionParticipantResponseModel> GetAuctionParticipant(AuctionParticipantRequestModel requestModel);

    Task<List<AuctionTeamResponseModel>> GetAllJoinedTeams(int auctionId);
    Task DeductUserBalance(DeductBalanceRequest request);
}
