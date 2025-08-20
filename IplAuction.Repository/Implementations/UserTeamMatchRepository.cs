using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;

namespace IplAuction.Repository.Implementations;

public class UserTeamMatchRepository(IplAuctionDbContext context) : GenericRepository<UserTeamMatch>(context), IUserTeamMatchRepository
{
}
