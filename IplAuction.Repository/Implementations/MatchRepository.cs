using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Repository.Implementations;

public class MatchRepository(IplAuctionDbContext context) : GenericRepository<Match>(context),IMatchRepository
{
}
