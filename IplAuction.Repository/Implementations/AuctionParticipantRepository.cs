using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Repository.Implementations;

public class AuctionParticipantRepository(IplAuctionDbContext dbContext) : GenericRepository<AuctionParticipants>(dbContext), IAuctionParticipantRepository
{
    
}
