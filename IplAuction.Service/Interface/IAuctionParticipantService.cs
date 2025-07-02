using IplAuction.Entities.Models;

namespace IplAuction.Service.Interface;

public interface IAuctionParticipantService
{
    Task AddParticipantAsync(int auctionId, int userId);

    Task AddParticipantsAsync(List<AuctionParticipants> auctionParticipants);

    Task RemoveParticipantsAsync(List<AuctionParticipants> auctionParticipants);
    Task<List<AuctionParticipants>> GetParticipantsByUserIdsAsync(int auctionid, List<int> auctionParticipants);
    Task<List<AuctionParticipants>> GetParticipantsByAuctionIdAsync(int auctionid);
}
