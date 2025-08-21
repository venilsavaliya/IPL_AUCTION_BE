using IplAuction.Entities;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.UserTeam;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IplAuction.Repository.Implementations;

public class UserTeamRepository(IplAuctionDbContext context) : GenericRepository<UserTeam>(context), IUserTeamRepository
{

    public async Task<List<UserTeamResponseModel>> GetUserTeams(UserTeamRequestModel request)
    {
        List<UserTeamResponseModel> userTeams = await _context.UserTeams.Include(ut => ut.Player).Where(ut => ut.AuctionId == request.AuctionId && ut.UserId == request.UserId && (ut.IsReshuffled == false && ut.ReshuffledStatus == false || ut.IsReshuffled == true && ut.ReshuffledStatus == true)).Select(ut => new UserTeamResponseModel
        {
            PlayerId = ut.PlayerId,
            Price = ut.Price,
            Skill = ut.Player.Skill,
            Name = ut.Player.Name,
            Image = ut.Player.Image ?? ""
        }).ToListAsync();

        return userTeams;
    }

    public async Task<List<UserTeamOfMatchResponseModel>> GetUserTeamOfMatch(UserTeamOfMatchRequestModel request)
    {
        List<UserTeamOfMatchResponseModel> userTeamsOfMatch = _context.UserTeamMatches.Include(ut => ut.Player).Where(ut => ut.AuctionId == request.AuctionId && ut.UserId == request.UserId).Select(ut => new UserTeamOfMatchResponseModel
        {
            PlayerId = ut.PlayerId,
            Name = ut.Player.Name,
            Image = ut.Player.Image ?? ""
        }).ToList();

        List<UserTeamOfMatchResponseModel> currentUserTeam = _context.UserTeams.Include(ut => ut.Player).Where(ut => ut.AuctionId == request.AuctionId && ut.UserId == request.UserId).Select(ut => new UserTeamOfMatchResponseModel
        {
            PlayerId = ut.PlayerId,
            Name = ut.Player.Name,
            Image = ut.Player.Image ?? ""
        }).ToList();

        return userTeamsOfMatch.UnionBy(currentUserTeam, ut => ut.PlayerId).ToList();
    }

    public async Task<List<UserTeam>> GetUserTeamsByPlayerIds(List<int> ids)
    {
        // ut.IsReshuffled == false && ut.ReshuffledStatus == false || ut.IsReshuffled == true && ut.ReshuffledStatus == true
        // Above Condition describe that Player is currently in the team of the user after and before reshuffled round happen.
        List<UserTeam> userTeams = await _context.UserTeams.Where(ut => ids.Contains(ut.PlayerId) && (ut.IsReshuffled == false && ut.ReshuffledStatus == false || ut.IsReshuffled == true && ut.ReshuffledStatus == true)).ToListAsync();

        return userTeams;
    }

    public async Task ReshufflePlayers(List<ReshufflePlayerRequest> request)
    {
        foreach (var req in request)
        {
            Auction auction = await _context.Auctions.FirstOrDefaultAsync(a => a.Id == req.AuctionId) ?? throw new NotFoundException(nameof(auction));
            if (auction.AuctionStatus != AuctionStatus.Reshuffling)
            {
                throw new Exception(ExceptionMessages.AuctionStatusIsNotReshuffling);
            }

            UserTeam ut = await _context.UserTeams.FirstOrDefaultAsync(ut => ut.PlayerId == req.PlayerId && ut.AuctionId == req.AuctionId && ut.UserId == req.UserId) ?? throw new NotFoundException(nameof(UserTeam));

            ut.IsReshuffled = true;
            ut.ReshuffledStatus = false; // false show that player is released from the team

            AuctionParticipants ap = await _context.AuctionParticipants.FirstOrDefaultAsync(ap => ap.UserId == req.UserId && ap.AuctionId == req.AuctionId) ?? throw new NotFoundException(nameof(AuctionParticipants));
            ap.PurseBalance += req.PlayerBoughtPrice;

            AuctionPlayer auctionPlayer = await _context.AuctionPlayers.FirstOrDefaultAsync(ap => ap.AuctionId == req.AuctionId && ap.PlayerId == req.PlayerId) ?? throw new NotFoundException(nameof(AuctionPlayer));
            auctionPlayer.IsAuctioned = false;
            auctionPlayer.IsSold = false;

            await _context.SaveChangesAsync();
        }
    }
}
