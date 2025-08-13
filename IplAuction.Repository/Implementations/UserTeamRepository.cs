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
}
