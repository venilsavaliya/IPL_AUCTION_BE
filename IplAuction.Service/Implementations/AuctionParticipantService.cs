using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class AuctionParticipantService(IAuctionParticipantRepository auctionParticipantRepo) : IAuctionParticipantService
{
    private readonly IAuctionParticipantRepository _auctionParticipantRepo = auctionParticipantRepo;

    public async Task AddParticipantAsync(int auctionId, int userId)
    {
        AuctionParticipants auctionParticipants = new()
        {
            UserId = userId,
            AuctionId = auctionId
        };

        await _auctionParticipantRepo.AddAsync(auctionParticipants);

        await _auctionParticipantRepo.SaveChangesAsync();
    }
}
