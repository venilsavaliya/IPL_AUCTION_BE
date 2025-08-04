using IplAuction.Entities.DTOs;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Entities.ViewModels.Match;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class MatchService(IMatchRepository matchRepository, IBallEventService ballEventService, IPlayerService playerService, IInningStateService inningStateService) : IMatchService
{

    private readonly IMatchRepository _matchRepository = matchRepository;
    private readonly IBallEventService _ballEventService = ballEventService;
    private readonly IPlayerService _playerService = playerService;
    private readonly IInningStateService _inningStateService = inningStateService;


    public async Task AddMatch(MatchRequest request)
    {
        Match match = new()
        {
            TeamAId = request.TeamAId,
            TeamBId = request.TeamBId,
            StartDate = request.StartDate,
            SeasonId = request.SeasonId
        };

        await _matchRepository.AddAsync(match);
        await _matchRepository.SaveChangesAsync();
    }

    public async Task UpdateMatch(UpdateMatchRequest request)
    {
        Match match = await _matchRepository.GetWithFilterAsync(m => m.Id == request.Id && m.IsDeleted != true) ?? throw new NotFoundException(nameof(Match));

        match.TeamAId = request.TeamAId;
        match.TeamBId = request.TeamBId;
        match.StartDate = request.StartDate;
        match.SeasonId = request.SeasonId;

        await _matchRepository.SaveChangesAsync();
    }

    public async Task UpdateMatchInningNumber(int matchId, int inningNumber)
    {
        Match match = await _matchRepository.GetWithFilterAsync(m => m.Id == matchId && m.IsDeleted != true) ?? throw new NotFoundException(nameof(Match));
        match.InningNumber = inningNumber;
        await _matchRepository.SaveChangesAsync();
    }

    public async Task DeleteMatch(int id)
    {
        Match match = await _matchRepository.GetWithFilterAsync(m => m.Id == id) ?? throw new NotFoundException(nameof(Match));

        match.IsDeleted = true;

        await _matchRepository.SaveChangesAsync();
    }

    public async Task<List<MatchResponse>> GetAllMatch()
    {
        List<MatchResponse> response = await _matchRepository.GetAllWithFilterAsync(m => m.IsDeleted == false, m => new MatchResponse()
        {
            MatchId = m.Id,
            TeamAId = m.TeamAId,
            TeamBId = m.TeamBId,
            TeamAName = m.TeamA.Name,
            TeamBName = m.TeamB.Name,
            StartDate = m.StartDate
        });
        return response;
    }

    public async Task<PaginatedResult<MatchResponse>> GetFilteredMatchAsync(MatchFilterParams filterParams)
    {
        PaginatedResult<MatchResponse> result = await _matchRepository.GetFilteredMatchAsync(filterParams);

        return result;
    }

    public async Task<MatchResponse> GetMatchById(int id)
    {
        return await _matchRepository.GetById(id);
    }

    public async Task ChangeMatchInning(int matchId, int inningNumber)
    {
        Match match = await _matchRepository.GetWithFilterAsync(m => m.Id == matchId && m.IsDeleted != true) ?? throw new NotFoundException(nameof(Match));
        match.InningNumber = inningNumber;
        await _matchRepository.SaveChangesAsync();
    }

    public async Task<LiveMatchStatusResponse> GetLiveMatchStatus(int matchId)
    {
        // 1. Fetch match and teams
        var match = await _matchRepository.GetWithFilterAsync(
            m => m.Id == matchId && m.IsDeleted == false,
            m => m.TeamA,
            m => m.TeamB
        );
        if (match == null)
            throw new NotFoundException("Match not found");

        InningState? inningState = await _inningStateService.GetInningState(matchId, match.InningNumber);

        // 2. Fetch all ball events for the match
        var allBalls = await _ballEventService.GetBallEventsForMatch(matchId);

        if (inningState == null)
        {
            // No balls bowled yet, return initial state
            return new LiveMatchStatusResponse
            {
                MatchId = match.Id,
                MatchStatus = "Not Started",
                TeamA = match.TeamA.Name,
                TeamB = match.TeamB.Name,
                TeamAId = match.TeamAId,
                TeamBId = match.TeamBId,
                InningStateId = 0,
                InningNumber = match.InningNumber,
                TotalRuns = 0,
                TotalWickets = 0,
                Overs = 0,
                RunRate = 0,
                CurrentBatsmen = new List<BatsmanStatus>(),
                CurrentBowler = null,
                RecentBalls = new List<BallSummary>()
            };
        }

        // 3. Determine current inning
        int currentInning = allBalls.Max(b => b.InningNumber);
        var inningBalls = allBalls.Where(b => b.InningNumber == currentInning).ToList();

        // 4. Aggregate runs, wickets, overs
        int totalRuns = inningBalls.Sum(b => b.RunsScored + b.ExtraRuns);
        int totalWickets = inningBalls.Count(b => b.WicketType != null);

        // Calculate overs using only legal deliveries
        int legalDeliveries = inningBalls.Count(b => b.ExtraType == null);
        int completedOvers = legalDeliveries / 6;
        int ballsInCurrentOver = legalDeliveries % 6;
        double overs = double.Parse($"{completedOvers}.{ballsInCurrentOver}");

        // 5. Current batsmen (use StrikerId and NonStrikerId from InningState)

        // var currentInningState = inningState.FirstOrDefault(i => i.InningNumber == currentInning);
        var currentBatsmen = new List<BatsmanStatus>();
        if (inningState != null)
        {
            var batsmanIds = new List<int> { inningState.StrikerId ?? 0, inningState.NonStrikerId ?? 0 };
            foreach (var batsmanId in batsmanIds)
            {
                if (batsmanId == 0)
                {
                    continue;
                }
                var batsmanBalls = inningBalls.Where(b => b.BatsmanId == batsmanId).ToList();

                if (batsmanBalls.Count == 0 || batsmanBalls == null) // Player dont play any ball yet
                {
                    // Try to get player name from PlayerService if not present in balls
                    var player = await _playerService.GetPlayerByIdAsync(batsmanId);
                    currentBatsmen.Add(new BatsmanStatus
                    {
                        PlayerId = batsmanId,
                        Name = player?.Name ?? "",
                        Runs = 0,
                        Balls = 0,
                        Fours = 0,
                        Sixes = 0,
                        IsOnStrike = batsmanId == inningState.StrikerId
                    });
                    continue;
                }
                var batsman = batsmanBalls.First().Batsman;

                currentBatsmen.Add(new BatsmanStatus
                {
                    PlayerId = batsman.Id,
                    Name = batsman.Name,
                    Runs = batsmanBalls.Sum(b => b.RunsScored),
                    Balls = batsmanBalls.Count(),
                    Fours = batsmanBalls.Count(b => b.RunsScored == 4),
                    Sixes = batsmanBalls.Count(b => b.RunsScored == 6),
                    IsOnStrike = batsmanId == inningState.StrikerId
                });
            }
        }

        // 6. Current bowler (last bowler ID)
        // var lastBallForBowler = inningBalls.OrderByDescending(b => b.OverNumber).ThenByDescending(b => b.BallNumber).FirstOrDefault();
        // var lastBallForBowler = await _inningStateService.GetByMatchIdAsync(matchId);
        BowlerStatus currentBowler = null;
        if (inningState != null && inningState.BowlerId != 0)
        {
            var bowlerTotalBalls = inningBalls.Where(b => b.BowlerId == inningState.BowlerId).ToList();

            if (inningState.BowlerId != null && inningState.BowlerId != 0)
            {
                var bowler = await _playerService.GetPlayerByIdAsync((int)inningState.BowlerId);

                int bowlerOvers = bowlerTotalBalls.Count(b => b.ExtraType == null) / 6;
                int bowlerBalls = bowlerTotalBalls.Count(b => b.ExtraType == null) % 6;

                currentBowler = new BowlerStatus
                {
                    PlayerId = bowler.PlayerId,
                    Name = bowler.Name,
                    Overs = bowlerOvers + (bowlerBalls / 10.0),
                    RunsConceded = bowlerTotalBalls.Sum(b => b.RunsScored + b.ExtraRuns),
                    Wickets = bowlerTotalBalls.Count(b => b.WicketType != null)
                };
            }
        }

        // 7. Run rate
        int totalLegalDeliveries = inningBalls.Count(b => b.ExtraType == null);
        double runRate = overs > 0 ? Math.Round((double)totalRuns / totalLegalDeliveries * 6, 2) : 0;

        // 8. Target and required run rate (if 2nd inning)
        int? target = null;
        double? requiredRunRate = null;
        if (currentInning == 2)
        {
            var firstInningRuns = allBalls.Where(b => b.InningNumber == 1).Sum(b => b.RunsScored + b.ExtraRuns);
            target = firstInningRuns + 1;
            int ballsBowled = inningBalls.Count(b => b.ExtraType == null);
            int ballsLeft = 120 - ballsBowled; // T20: 20 overs * 6 balls
            requiredRunRate = ballsLeft > 0 ? ((target.Value - totalRuns) * 6.0) / ballsLeft : 0;
        }

        // 9. Recent balls (current over)
        var recentBalls = (await _ballEventService.GetRecentBalls(matchId, currentInning, 0, true))
            .Select(b => new BallSummary
            {
                OverNumber = b.OverNumber,
                BallNumber = b.BallNumber,
                Result = b.WicketType != null ? "W" : (b.ExtraType != null ? b.ExtraType.ToString() : b.RunsScored.ToString()) ?? "",
                IsLegalDelivery = b.ExtraType == null
            }).ToList();

        // 10. Build and return response
        return new LiveMatchStatusResponse
        {
            MatchId = match.Id,
            MatchStatus = "In Progress", // You can enhance this logic
            TeamA = match.TeamA.Name,
            TeamB = match.TeamB.Name,
            InningNumber = currentInning,
            TeamAId = match.TeamAId,
            TeamBId = match.TeamBId,
            BattingTeamId = (int)inningState.BattingTeamId,
            BowlingTeamId = (int)inningState.BowlingTeamId,
            TotalRuns = totalRuns,
            TotalWickets = totalWickets,
            Overs = overs,
            Target = target,
            RequiredRunRate = requiredRunRate,
            RunRate = runRate,
            CurrentBatsmen = currentBatsmen,
            CurrentBowler = currentBowler,
            RecentBalls = recentBalls,
            InningStateId = inningState.Id
        };
    }

    public async Task<int> GetSeasonIdFromMatchId(int matchId)
    {
        Match match = await _matchRepository.GetWithFilterAsync(a => a.Id == matchId && a.IsDeleted != true) ?? throw new NotFoundException(nameof(Match));

        return match.SeasonId;
    }

    public async Task<List<AuctionParticipantMantchDetail>> GetAuctionParticipantMantchDetailsAsync(int auctionId, int userId)
    {
        return await _matchRepository.GetAuctionParticipantMantchDetailsAsync(auctionId, userId);
    }
}
