using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.AuctionPlayer;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace IplAuction.Repository.Implementations;

public class AuctionPlayerRepository(IplAuctionDbContext context) : GenericRepository<AuctionPlayer>(context), IAuctionPlayerRepository
{
    public PaginatedResult<AuctionPlayerDetail> GetAuctionPlayerDetailList(AuctionPlayerFilterParams request)
    {
        int auctionId = request.AuctionId;

        // var query = _context.Players.
        //                     Where(p=>p.IsDeleted == false)
        //                     .GroupJoin(
        //                         _context.AuctionPlayers.Where(ap => ap.AuctionId == auctionId),
        //                         t1 => t1.Id,
        //                         t2 => t2.PlayerId,
        //                         (t1, t2Group) => new { t1, t2Group }
        //                     ).SelectMany(
        //                         x => x.t2Group.DefaultIfEmpty(),
        //                         (x, t2) => new { x.t1, t2 }
        //                     ).GroupJoin(
        //                         _context.UserTeams.Include(ut => ut.User).Where(ut => ut.AuctionId == auctionId && (ut.IsReshuffled == true && ut.ReshuffledStatus == false || ut.IsReshuffled == false && ut.ReshuffledStatus == true)),
        //                         x => x.t1.Id,
        //                         t3 => t3.PlayerId,
        //                         (x, t3Group) => new { x.t1, x.t2, t3Group }
        //                     ).SelectMany(
        //                        x => x.t3Group.DefaultIfEmpty(),
        //                             (x, t3) => new
        //                             {
        //                                 x.t1,
        //                                 x.t2,
        //                                 t3
        //                             }
        //                     ).AsEnumerable().Select(x =>
        //                        {
        //                            bool isReshuffledStatus = x.t3?.IsReshuffled??false && x.t2?.IsAuctioned == false;
        //                            bool isAuctioned = x.t2?.IsAuctioned ?? false;
        //                            var status = isReshuffledStatus ? AuctionPlayerStatus.Reshuffled : isAuctioned
        //                                ? (x.t2?.IsSold ?? false ? AuctionPlayerStatus.Sold : AuctionPlayerStatus.UnSold)
        //                                : AuctionPlayerStatus.UnAuctioned;

        //                            return new AuctionPlayerDetail
        //                            {
        //                                PlayerId = x.t1.Id,
        //                                PlayerName = x.t1.Name,
        //                                PlayerSkill = x.t1.Skill,
        //                                Status = status,
        //                                SoldPrice = x.t3?.Price ?? 0,
        //                                SoldTo = x.t3?.User.FirstName + " " + x.t3?.User.LastName
        //                            };
        //                        }
        //                     ).AsQueryable();

        var query = (from p in _context.Players
                     where p.IsDeleted == false
                     join
                    ap in _context.AuctionPlayers.Where(ap => ap.AuctionId == auctionId)
                    on p.Id equals ap.PlayerId into apgroup
                     from ap in apgroup.DefaultIfEmpty()
                     join
                     ut in _context.UserTeams.Where(ut => ut.AuctionId == auctionId) on ap.PlayerId equals ut.PlayerId into utgroup
                     from ut in utgroup.DefaultIfEmpty()
                     select new AuctionPlayerDetail
                     {
                         PlayerId = p.Id,
                         PlayerName = p.Name,
                         PlayerSkill = p.Skill,
                         Status =
    (ut != null && ap != null && ut.IsReshuffled && !ap.IsAuctioned)
        ? AuctionPlayerStatus.Reshuffled
        : (ap == null
            ? AuctionPlayerStatus.UnAuctioned
            : (ap.IsAuctioned
                ? (ap.IsSold ? AuctionPlayerStatus.Sold : AuctionPlayerStatus.UnSold)
                : AuctionPlayerStatus.UnAuctioned)),
                         SoldPrice = ut != null ? ut.Price : 0,
                         SoldTo = ut != null ? ut.User.FirstName + " " + ut.User.LastName : null
                     }).AsQueryable();

        // Player Name Filter

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(p => p.PlayerName.ToLower().Contains(request.Name.ToLower().Trim()));
        }

        // Player Skill Filter

        if (!string.IsNullOrEmpty(request.Skill))
        {
            string role = request.Skill.ToLower();

            if (Enum.TryParse<PlayerSkill>(request.Skill, true, out var skillEnum))
            {
                query = query.Where(u => u.PlayerSkill == skillEnum);
            }
        }

        // Player Status Filter

        if (!string.IsNullOrEmpty(request.Status))
        {
            string role = request.Status.ToLower();

            if (Enum.TryParse<AuctionPlayerStatus>(request.Status, true, out var statusEnum))
            {
                query = query.Where(u => u.Status == statusEnum);
            }
        }

        // Sorting
        var allowedSorts = new[] { "PlayerName", "PlayerSkill", "Status", "SoldPrice" };
        var sortBy = allowedSorts.Contains(request.SortBy) ? request.SortBy : "PlayerName";
        var sortDirection = request.SortDirection?.ToLower() == "desc" ? "desc" : "asc";

        query = query.OrderBy($"{sortBy} {sortDirection}");

        PaginationParams paginationParams = new()
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        PaginatedResult<AuctionPlayerDetail> paginatedResult = query.ToPaginatedList(paginationParams, p => new AuctionPlayerDetail
        {
            PlayerId = p.PlayerId,
            PlayerName = p.PlayerName,
            PlayerSkill = p.PlayerSkill,
            SoldPrice = p.SoldPrice,
            Status = p.Status,
            SoldTo = p.SoldTo
        });

        return paginatedResult;
    }
}
