using System.Data.Common;
using IplAuction.Entities;
using IplAuction.Entities.Enums;
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

    public async Task<List<AuctionTeamResponseModel>> GetAllJoinedTeams(int auctionId)
    {
        return await _context.AuctionParticipants
                        .Where(ap => ap.AuctionId == auctionId)
                        .Select(ap => new AuctionTeamResponseModel
                        {
                            UserId = ap.UserId,
                            FullName = $"{ap.User.FirstName} {ap.User.LastName ?? ""}",
                            AuctionId = ap.AuctionId,
                            ImageUrl = ap.User.Image ?? "",
                            BalanceLeft = ap.PurseBalance,
                            TotalPlayers = ap.User.UserTeams.Count()
                        }).ToListAsync();

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

    public async Task<List<AuctionParticipantDetail>> GetAuctionParticipantsDetailList(AuctionParticipantDetailRequestModel request)
    {
        var auctionId = request.AuctionId;
        var seasonId = request.SeasonId;

        // Load config in memory
        var config = await _context.ScoringRules
            .ToDictionaryAsync(c => c.EventType, c => c.Points);

        // Step 1: Load data first
        var participants = await _context.AuctionParticipants
     .Where(ap => ap.AuctionId == auctionId)
     .Include(ap => ap.User)
         .ThenInclude(u => u.UserTeams)
             .ThenInclude(ut => ut.Player)
                 .ThenInclude(p => p.PlayerMatchStates)
                     .ThenInclude(pms => pms.Match)
     .ToListAsync();


        // Step 2: Calculate points in-memory
        var result = participants.Select(ap =>
        {
            var playerStats = ap.User?.UserTeams?
                .Where(ut => ut.Player != null && ut.Player.PlayerMatchStates != null && ut.AuctionId == auctionId)
                .SelectMany(ut => ut.Player.PlayerMatchStates
                    .Where(pms => pms.Match != null && pms.Match.SeasonId == seasonId))
                ?? [];

            var points = playerStats.Sum(pms =>
                (pms.Runs * config.GetValueOrDefault(CricketEventType.Run)) +
                (pms.Fours * config.GetValueOrDefault(CricketEventType.Four)) +
                (pms.Sixes * config.GetValueOrDefault(CricketEventType.Six)) +
                (pms.Wickets * config.GetValueOrDefault(CricketEventType.Wicket)) +
                (pms.Catches * config.GetValueOrDefault(CricketEventType.Catch)) +
                (pms.Stumpings * config.GetValueOrDefault(CricketEventType.Stumping)) +
                (pms.RunOuts * config.GetValueOrDefault(CricketEventType.RunOut)) +
                (pms.MaidenOvers * config.GetValueOrDefault(CricketEventType.MaidenOver))
            );

            return new AuctionParticipantDetail
            {
                AuctionId = ap.AuctionId,
                UserId = ap.UserId,
                UserName = ap.User?.FirstName + " " + ap.User?.LastName,
                ImageUrl = ap.User?.Image,
                TotalPlayers = ap.User != null ? ap.User.UserTeams.Count(ut => ut.AuctionId == auctionId) : 0,
                Points = points
            };
        }).OrderByDescending(r => r.Points).ToList();

        return result;
    }

}
