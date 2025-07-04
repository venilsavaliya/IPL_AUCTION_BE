using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Repository.Implementations;

public class BidRepository(IplAuctionDbContext context): GenericRepository<Bid>(context), IBidRepository
{
    
}
