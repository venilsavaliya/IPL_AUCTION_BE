using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Repository.Implementations;

public class SeasonRepository(IplAuctionDbContext context) : GenericRepository<Season>(context), ISeasonRepository
{
}
