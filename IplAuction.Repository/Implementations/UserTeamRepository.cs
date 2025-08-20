using IplAuction.Entities;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.UserTeam;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IplAuction.Repository.Implementations;

public class UserTeamRepository(IplAuctionDbContext context) : GenericRepository<UserTeam>(context), IUserTeamRepository
{

    public async Task<List<UserTeamResponseModel>> GetUserTeams(UserTeamRequestModel request)
    {
        List<UserTeamResponseModel> userTeams = await _context.UserTeams.Include(ut => ut.Player).Where(ut => ut.AuctionId == request.AuctionId && ut.UserId == request.UserId).Select(ut => new UserTeamResponseModel
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

        return userTeamsOfMatch.UnionBy(currentUserTeam,ut=>ut.PlayerId).ToList();
    }

    public async Task<List<UserTeam>> GetUserTeamsByPlayerIds(List<int> ids)
    {
        List<UserTeam> userTeams = await _context.UserTeams.Where(ut => ids.Contains(ut.PlayerId)).ToListAsync();

        return userTeams;
    }
}
