using System.Data.Common;
using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IplAuction.Repository.Implementations;

public class AuctionParticipantRepository(IplAuctionDbContext dbContext) : GenericRepository<AuctionParticipants>(dbContext), IAuctionParticipantRepository
{
    public async Task<List<UserResponseViewModel>> GetAllParticipantsByAuctionIdAsync(int auctionId)
    {
        return await _context.AuctionParticipants.Include(ap => ap.User).Where(ap => ap.AuctionId == auctionId)
                .Select(ap => new UserResponseViewModel(ap.User)).ToListAsync();
    }
}
