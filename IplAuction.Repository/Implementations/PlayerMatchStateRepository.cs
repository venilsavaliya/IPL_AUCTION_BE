using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Repository.Implementations;

public class PlayerMatchStateRepository(IplAuctionDbContext context):GenericRepository<PlayerMatchStates>(context),IPlayerMatchStateRepository
{
}
