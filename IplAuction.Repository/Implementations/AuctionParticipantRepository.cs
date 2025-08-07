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

    public async Task<AuctionParticipantAllDetail> GetAllDetailOfAuctionParticipant(AuctionParticipantAllDetailRequestModel request)
    {
        Auction auction = await _context.Auctions.FirstOrDefaultAsync(s => s.Id == request.AuctionId) ?? throw new NotFoundException(nameof(Auction));

        int seasonId = auction.SeasonId;

        int totalParticipants = _context.AuctionParticipants.Where(ap => ap.AuctionId == request.AuctionId).Count();

        int totalPlayers = _context.UserTeams.Where(ut => ut.UserId == request.UserId && ut.AuctionId == request.AuctionId).Count();

        var resultData = _context.Users.Where(ap => ap.Id == request.UserId).Select(u => new AuctionParticipantAllDetail
        {
            AuctionId = request.AuctionId,
            ImageUrl = u.Image,
            UserId = request.UserId,
            UserName = u.FirstName + " " + u.LastName,
            BalanceLeft = u.AuctionParticipants.Where(ap => ap.AuctionId == request.AuctionId && ap.UserId == request.UserId).Select(ap => ap.PurseBalance).FirstOrDefault(),
            TotalPlayers = totalPlayers,
            TotalParticipants = totalParticipants,
            Rank = 0,
            Points = 0
        }).FirstOrDefault() ?? new AuctionParticipantAllDetail();

        var config = await _context.ScoringRules.ToDictionaryAsync(s => s.EventType, s => s.Points);

        var userWiseData = await
        (from ut in _context.UserTeams where ut.AuctionId == request.AuctionId
         join pms in _context.PlayerMatchStates on ut.PlayerId equals pms.PlayerId
         where pms.Match.SeasonId == seasonId
         group new { pms } by new { ut.UserId, ut.AuctionId } into g
         select new
         {
             g.Key.UserId,
             g.Key.AuctionId,
             Fours = g.Sum(x => x.pms.Fours),
             Sixes = g.Sum(x => x.pms.Sixes),
             Wickets = g.Sum(x => x.pms.Wickets),
             Catches = g.Sum(x => x.pms.Catches),
             Stumpings = g.Sum(x => x.pms.Stumpings),
             RunOuts = g.Sum(x => x.pms.RunOuts),
             MaidenOvers = g.Sum(x => x.pms.MaidenOvers),
             Runs = g.Sum(x => x.pms.Runs),
         }
        ).ToListAsync();

        var userWiseTotalPoints = userWiseData.Select(x => new
        {
            x.UserId,
            Points = x.Fours * config.GetValueOrDefault(CricketEventType.Four) +
                        x.Sixes * config.GetValueOrDefault(CricketEventType.Six) +
                        x.Wickets * config.GetValueOrDefault(CricketEventType.Wicket) +
                        x.Catches * config.GetValueOrDefault(CricketEventType.Catch) +
                        x.Stumpings * config.GetValueOrDefault(CricketEventType.Stumping) +
                        x.RunOuts * config.GetValueOrDefault(CricketEventType.RunOut) +
                        x.MaidenOvers * config.GetValueOrDefault(CricketEventType.MaidenOver) +
                        x.Runs * config.GetValueOrDefault(CricketEventType.Run)
        }).OrderByDescending(x => x.Points).ToList();

        var index = 0;
        var userWiseTotalPointsData = userWiseTotalPoints.Select(x =>
        {
            index++;
            return new
            {
                x.UserId,
                x.Points,
                Rank = index
            };
        }).ToList();

        var matchWiseTotalPointsData = await
        (from ut in _context.UserTeams
         where ut.UserId == request.UserId && ut.AuctionId == request.AuctionId
         join pms in _context.PlayerMatchStates on ut.PlayerId equals pms.PlayerId
         where pms.Match.SeasonId == seasonId
         group new { pms } by new { pms.MatchId, ut.UserId, pms.PlayerId } into g
         select new
         {
             g.Key.MatchId,
             g.Key.UserId,
             g.Key.PlayerId,
             Fours = g.Sum(x => x.pms.Fours),
             Sixes = g.Sum(x => x.pms.Sixes),
             Wickets = g.Sum(x => x.pms.Wickets),
             Catches = g.Sum(x => x.pms.Catches),
             Stumpings = g.Sum(x => x.pms.Stumpings),
             RunOuts = g.Sum(x => x.pms.RunOuts),
             MaidenOvers = g.Sum(x => x.pms.MaidenOvers),
             Runs = g.Sum(x => x.pms.Runs),
         }
        ).ToListAsync();

        int bestScore = 0;

        var matchWiseTotalPoints = matchWiseTotalPointsData.Select(x =>
        {
            int points = x.Fours * config.GetValueOrDefault(CricketEventType.Four) +
                x.Sixes * config.GetValueOrDefault(CricketEventType.Six) +
                x.Wickets * config.GetValueOrDefault(CricketEventType.Wicket) +
                x.Catches * config.GetValueOrDefault(CricketEventType.Catch) +
                x.Stumpings * config.GetValueOrDefault(CricketEventType.Stumping) +
                x.RunOuts * config.GetValueOrDefault(CricketEventType.RunOut) +
                x.MaidenOvers * config.GetValueOrDefault(CricketEventType.MaidenOver) +
                x.Runs * config.GetValueOrDefault(CricketEventType.Run);

            if (x.UserId == request.UserId && points > bestScore)
            {
                bestScore = points;
            }

            return new
            {
                x.MatchId,
                x.UserId,
                Points = points
            };
        }).ToList();

        resultData.Points = matchWiseTotalPoints.Where(x => x.UserId == request.UserId).Sum(x => x.Points);
        resultData.BestScore = bestScore;
        resultData.Rank = userWiseTotalPointsData.Where(x => x.UserId == request.UserId).Select(x => x.Rank).FirstOrDefault();

        return resultData;
    }

    public async Task<AuctionParticipantPlayerResponseModel> GetParticipantsPlayerListAndDetail(ParticipantPlayerRequestModel request)
    {
        Auction auction = _context.Auctions.FirstOrDefault(a => a.Id == request.AuctionId) ?? throw new NotFoundException(nameof(Auction));

        int seasonId = auction.SeasonId;

        var data = await _context.AuctionParticipants.Where(ap => ap.UserId == request.UserId && ap.AuctionId == request.AuctionId).Select(x => new
        AuctionParticipantPlayerResponseModel
        {
            UserId = request.UserId,
            TotalAmountSpent = auction.MaximumPurseSize - x.PurseBalance,
            TotalPlayers = x.User.UserTeams.Count(ut => ut.UserId == request.UserId && ut.AuctionId == request.AuctionId),
            TotalPoints = 0,
            ParticipantsPlayers = new List<ParticipantsPlayer>()
        }).FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(AuctionParticipants));

        var config = await _context.ScoringRules.ToDictionaryAsync(x => x.EventType, x => x.Points);

        var playersStates = await (
                from ut in _context.UserTeams
                where ut.UserId == request.UserId && ut.AuctionId == request.AuctionId
                join pms in _context.PlayerMatchStates
                    on ut.PlayerId equals pms.PlayerId into pmsGroup
                from pms in pmsGroup.DefaultIfEmpty() // <-- left join
                where pms == null || pms.Match.SeasonId == seasonId
                group new { pms } by new { ut.PlayerId } into g
                select new
                {
                    g.Key.PlayerId,
                    Runs = g.Sum(x => x.pms != null ? x.pms.Runs : 0),
                    Fours = g.Sum(x => x.pms != null ? x.pms.Fours : 0),
                    Sixes = g.Sum(x => x.pms != null ? x.pms.Sixes : 0),
                    Wickets = g.Sum(x => x.pms != null ? x.pms.Wickets : 0),
                    Catches = g.Sum(x => x.pms != null ? x.pms.Catches : 0),
                    Stumpings = g.Sum(x => x.pms != null ? x.pms.Stumpings : 0),
                    RunOuts = g.Sum(x => x.pms != null ? x.pms.RunOuts : 0),
                    MaidenOvers = g.Sum(x => x.pms != null ? x.pms.MaidenOvers : 0)
                }
            ).ToListAsync();


        int totalPoints = 0;
        var playersPoint = playersStates.Select(x =>
        {
            var Points = x.Fours * config.GetValueOrDefault(CricketEventType.Four) +
            x.Sixes * config.GetValueOrDefault(CricketEventType.Six) +
            x.Wickets * config.GetValueOrDefault(CricketEventType.Wicket) +
            x.Catches * config.GetValueOrDefault(CricketEventType.Catch) +
            x.Stumpings * config.GetValueOrDefault(CricketEventType.Stumping) +
            x.RunOuts * config.GetValueOrDefault(CricketEventType.RunOut) +
            x.MaidenOvers * config.GetValueOrDefault(CricketEventType.MaidenOver) +
            x.Runs * config.GetValueOrDefault(CricketEventType.Run);

            totalPoints += Points;

            return new
            {
                x.PlayerId,
                Points
            };
        }).ToDictionary(x => x.PlayerId, x => x.Points);


        List<ParticipantsPlayer> tempParticipantsPlayers = await _context.UserTeams.Where(ap => ap.UserId == request.UserId && ap.AuctionId == request.AuctionId).Select(x =>

            new ParticipantsPlayer
            {
                PlayerId = x.PlayerId,
                PlayerImage = x.Player.Image,
                PlayerName = x.Player.Name,
                PlayerPrice = (int)x.Player.BasePrice,
                PlayerBoughtPrice = x.Price,
                PlayerSkill = x.Player.Skill,
                PlayerPoints = 0,
                PlayersTotalMatches = x.Player.PlayerMatchStates.Count(x => x.Match.SeasonId == seasonId)
            }
        ).ToListAsync();

        List<ParticipantsPlayer> participantPlayersList = tempParticipantsPlayers.Select(x => new ParticipantsPlayer
        {
            PlayerId = x.PlayerId,
            PlayerImage = x.PlayerImage,
            PlayerName = x.PlayerName,
            PlayerBoughtPrice = x.PlayerBoughtPrice,
            PlayerPoints = playersPoint.GetValueOrDefault(x.PlayerId),
            PlayerPrice = x.PlayerPrice,
            PlayerSkill = x.PlayerSkill,
            PlayersTotalMatches = x.PlayersTotalMatches
        }).ToList();

        data.TotalPoints = totalPoints;
        data.ParticipantsPlayers = participantPlayersList;

        return data;
    }
}
