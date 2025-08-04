using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Entities.ViewModels.Match;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace IplAuction.Repository.Implementations;

public class MatchRepository(IplAuctionDbContext context) : GenericRepository<Match>(context), IMatchRepository
{
    public async Task<PaginatedResult<MatchResponse>> GetFilteredMatchAsync(MatchFilterParams filterParams)
    {
        var query = _context.Matches.Where(a => a.IsDeleted != true);

        // Search
        if (!string.IsNullOrWhiteSpace(filterParams.Search))
        {
            string search = filterParams.Search.ToLower();

            query = query.Where(m =>
                m.TeamA.Name.ToLower().Contains(search) || m.TeamB.Name.ToLower().Contains(search));
        }

        //Filtering Date
        if (filterParams.FromDate.HasValue && filterParams.ToDate.HasValue)
        {
            var fromDateUtc = DateTime.SpecifyKind(filterParams.FromDate.Value, DateTimeKind.Utc);
            var toDateUtc = DateTime.SpecifyKind(filterParams.ToDate.Value, DateTimeKind.Utc);

            query = query.Where(u => u.StartDate >= fromDateUtc && u.StartDate <= toDateUtc);
        }

        // Filtering Season 
        if (filterParams.SeasonId != null)
        {
            query = query.Where(p => p.SeasonId == filterParams.SeasonId);
        }

        // Sorting
        var allowedSorts = new[] { "StartDate" };
        var sortBy = allowedSorts.Contains(filterParams.SortBy) ? filterParams.SortBy : "Id";
        var sortDirection = filterParams.SortDirection?.ToLower() == "asc" ? "asc" : "desc";
        query = query.OrderBy($"{sortBy} {sortDirection}");

        PaginationParams paginationParams = new()
        {
            PageNumber = filterParams.PageNumber,
            PageSize = filterParams.PageSize
        };

        // Pagination
        PaginatedResult<MatchResponse> paginatedResult = await query.ToPaginatedListAsync(paginationParams, u => new MatchResponse
        {
            TeamAId = u.TeamAId,
            TeamBId = u.TeamBId,
            TeamAName = u.TeamA.Name,
            TeamBName = u.TeamB.Name,
            MatchId = u.Id,
            StartDate = u.StartDate
        });

        return paginatedResult;
    }

    public async Task<List<AuctionParticipantMantchDetail>> GetAuctionParticipantMantchDetailsAsync(int auctionId, int userId)
    {
        Auction auction = _context.Auctions.FirstOrDefault(a => a.Id == auctionId && a.IsDeleted != true) ?? throw new NotFoundException(nameof(Auction));

        int seasonId = auction.SeasonId;

        var config = await _context.ScoringRules.ToDictionaryAsync(s => s.EventType, s => s.Points);

        var matches = _context.Matches.Where(m => m.SeasonId == seasonId && m.IsDeleted != true).Select(m => new
        {
            MatchId = m.Id,
            TeamName = m.TeamA.Name + " vs " + m.TeamB.Name,
            Date = m.StartDate
        }).ToList();

        var allUsersData = await (
            from ut in _context.UserTeams
            join pms in _context.PlayerMatchStates on ut.PlayerId equals pms.PlayerId
            where ut.AuctionId == auctionId
            select new
            {
                pms.MatchId,
                pms.PlayerId,
                pms.Fours,
                pms.Sixes,
                pms.Runs,
                pms.RunOuts,
                pms.MaidenOvers,
                pms.Catches,
                pms.Stumpings,
                pms.Wickets,
                ut.UserId
            }
        ).ToListAsync();

        var allUserPointsPerMatch = allUsersData
     .GroupBy(x => (x.MatchId, x.UserId))
     .Select(g => new
     {
         g.Key.MatchId,
         g.Key.UserId,
         UserPoints = g.Sum(x =>
             x.Runs * config.GetValueOrDefault(CricketEventType.Run) +
             x.Fours * config.GetValueOrDefault(CricketEventType.Four) +
             x.Sixes * config.GetValueOrDefault(CricketEventType.Six) +
             x.Wickets * config.GetValueOrDefault(CricketEventType.Wicket) +
             x.Catches * config.GetValueOrDefault(CricketEventType.Catch) +
             x.Stumpings * config.GetValueOrDefault(CricketEventType.Stumping) +
             x.MaidenOvers * config.GetValueOrDefault(CricketEventType.MaidenOver) +
             x.RunOuts * config.GetValueOrDefault(CricketEventType.RunOut)
         )
     })
     .ToDictionary(x => (x.MatchId, x.UserId), x => x.UserPoints);

        var data = await (
            from ut in _context.UserTeams
            join pms in _context.PlayerMatchStates on ut.PlayerId equals pms.PlayerId
            where ut.AuctionId == auctionId && ut.UserId == userId
            select new
            {
                pms.MatchId,
                pms.PlayerId,
                pms.Fours,
                pms.Sixes,
                pms.Runs,
                pms.RunOuts,
                pms.MaidenOvers,
                pms.Catches,
                pms.Stumpings,
                pms.Wickets,
                ut.UserId
            }
        ).ToListAsync();

        var userPointsPerMatch = data.GroupBy(x => x.MatchId)
        .Select(g => new
        {
            MatchId = g.Key,
            UserPoints = g.Sum(x => x.Runs * config.GetValueOrDefault(CricketEventType.Run) +
                                    x.Fours * config.GetValueOrDefault(CricketEventType.Four) +
                                    x.Sixes * config.GetValueOrDefault(CricketEventType.Six) +
                                    x.Wickets * config.GetValueOrDefault(CricketEventType.Wicket) +
                                    x.Catches * config.GetValueOrDefault(CricketEventType.Catch) +
                                    x.Stumpings * config.GetValueOrDefault(CricketEventType.Stumping) +
                                    x.MaidenOvers * config.GetValueOrDefault(CricketEventType.MaidenOver) +
                                    x.RunOuts * config.GetValueOrDefault(CricketEventType.RunOut))
        }).ToDictionary(x => x.MatchId, x => x.UserPoints);

        var totalData = await (
            from pms in _context.PlayerMatchStates
            where pms.Match.SeasonId == seasonId && pms.Match.IsDeleted != true
            select new
            {
                pms.MatchId,
                pms.PlayerId,
                pms.Fours,
                pms.Sixes,
                pms.Runs,
                pms.RunOuts,
                pms.MaidenOvers,
                pms.Catches,
                pms.Stumpings,
                pms.Wickets,
            }
        ).ToListAsync();

        var totalPointsPerMatch = data.GroupBy(x => x.MatchId)
        .Select(g => new
        {
            MatchId = g.Key,
            Points = g.Sum(x => x.Runs * config.GetValueOrDefault(CricketEventType.Run) +
                                    x.Fours * config.GetValueOrDefault(CricketEventType.Four) +
                                    x.Sixes * config.GetValueOrDefault(CricketEventType.Six) +
                                    x.Wickets * config.GetValueOrDefault(CricketEventType.Wicket) +
                                    x.Catches * config.GetValueOrDefault(CricketEventType.Catch) +
                                    x.Stumpings * config.GetValueOrDefault(CricketEventType.Stumping) +
                                    x.MaidenOvers * config.GetValueOrDefault(CricketEventType.MaidenOver) +
                                    x.RunOuts * config.GetValueOrDefault(CricketEventType.RunOut))
        }).ToDictionary(x => x.MatchId, x => x.Points);

        var result = matches.Select(x =>
 {
     var userPoints = userPointsPerMatch.GetValueOrDefault(x.MatchId);
     var totalPoints = totalPointsPerMatch.GetValueOrDefault(x.MatchId);

     // Get all users' points for this match
     var usersInMatch = allUserPointsPerMatch
         .Where(kvp => kvp.Key.MatchId == x.MatchId)
         .OrderByDescending(kvp => kvp.Value)
         .ToList();

     // Assign rank based on position (starting at 1)
     var rank = usersInMatch
         .Select((kvp, index) => new { kvp.Key.UserId, Rank = index + 1 })
         .FirstOrDefault(r => r.UserId == userId)?.Rank ?? 0;

     return new AuctionParticipantMantchDetail
     {
         MatchId = x.MatchId,
         Date = x.Date,
         TeamName = x.TeamName,
         UserPoints = userPoints,
         Share = totalPoints > 0
             ? Math.Round(userPoints * 100.0 / totalPoints, 2)
             : 0,
         Rank = rank
     };
 }).ToList();


        return result;
    }

    public async Task<MatchResponse> GetById(int id)
    {
        MatchResponse result = await _context.Matches.Where(m => m.Id == id && m.IsDeleted != true).Select(m => new MatchResponse
        {
            MatchId = m.Id,
            StartDate = m.StartDate,
            TeamAId = m.TeamAId,
            TeamBId = m.TeamBId,
            TeamAName = m.TeamA.Name,
            TeamBName = m.TeamB.Name
        }).FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Match));

        return result;
    }
}
