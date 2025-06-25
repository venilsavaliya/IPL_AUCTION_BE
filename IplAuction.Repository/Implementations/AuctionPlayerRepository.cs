using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Repository.Implementations;

public class AuctionPlayerRepository(IplAuctionDbContext context) : GenericRepository<AuctionPlayer>(context), IAuctionPlayerRepository
{
    
}
