using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Repository.Implementations;

public class BallEventRepository(IplAuctionDbContext context):GenericRepository<BallEvent>(context),IBallEventRepository
{
}
