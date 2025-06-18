using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Repository.Implementations;

public class PlayerRepository(IplAuctionDbContext context) : GenericRepository<Player>(context), IPlayerRepository
{
    
}