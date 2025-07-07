using System.Data.Common;
using IplAuction.Entities;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.AuctionParticipant;
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

    public async Task<List<AuctionParticipantResponseModel>> GetAuctionParticipants(int auctionId)
    {
        var participants = await _context.AuctionParticipants
            .Include(ap => ap.User)
            .Where(ap => ap.AuctionId == auctionId)
            .Select(ap => new AuctionParticipantResponseModel
            {
                UserId = ap.UserId,
                FullName = $"{ap.User.FirstName} {ap.User.LastName ?? ""}",
                AuctionId = ap.AuctionId,
                Image = ap.User.Image ?? "",
                PurseBalance = ap.PurseBalance
            })
            .ToListAsync();

        return participants;
    }

    public async Task<AuctionParticipantResponseModel> GetAuctionParticipant(AuctionParticipantRequestModel request)
    {
        var participant = await _context.AuctionParticipants
            .Include(ap => ap.User)
            .Where(ap => ap.AuctionId == request.AuctionId && ap.UserId == request.UserId)
            .Select(ap => new AuctionParticipantResponseModel
            {
                UserId = ap.UserId,
                FullName = $"{ap.User.FirstName} {ap.User.LastName ?? ""}",
                AuctionId = ap.AuctionId,
                Image = ap.User.Image ?? "",
                PurseBalance = ap.PurseBalance
            })
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(AuctionParticipants));

        return participant;
    }
}
