using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Repository.Implementations;

public class InningStateRepository(IplAuctionDbContext context) : GenericRepository<InningState>(context), IInningStateRepository
{

} 