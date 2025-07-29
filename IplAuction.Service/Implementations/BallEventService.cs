using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.BallEvent;
using IplAuction.Entities.ViewModels.InningState;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class BallEventService(IBallEventRepository ballEventRepository, IInningStateService inningStateService) : IBallEventService
{
    private readonly IBallEventRepository _ballEventRepository = ballEventRepository;
    private readonly IInningStateService _inningStateService = inningStateService;

    // private readonly IMatchService _matchService = matchService;

    public async Task AddBall(AddBallEventRequest request)
    {
        BallEvent ball = new(request);

        await _ballEventRepository.AddAsync(ball);
        await _ballEventRepository.SaveChangesAsync();

        // Swap strike if needed
        bool isLegal = ball.ExtraType == null;
        if (isLegal)
        {
            // Odd runs: swap strike
            if (ball.RunsScored % 2 == 1)
            {
                await _inningStateService.SwapStrikeAsync(ball.MatchId, ball.InningNumber);
            }
            // End of over: swap strike and remove bowler
            var overBalls = await _ballEventRepository.GetAllWithFilterAsync(b => b.MatchId == ball.MatchId && b.InningNumber == ball.InningNumber && b.OverNumber == ball.OverNumber);
            // Count legal deliveries in this over
            int legalDeliveries = overBalls.Count(b => b.ExtraType == null);
            // If wicket, remove dismissed batsman
            if (ball.WicketType != null && ball.DismissedPlayerId.HasValue)
            {
                await _inningStateService.RemoveBatsmanAsync(ball.MatchId, ball.InningNumber, ball.DismissedPlayerId.Value);
            }
            if (legalDeliveries == 6)
            {
                await _inningStateService.SwapStrikeAsync(ball.MatchId, ball.InningNumber);

                await _inningStateService.UpdateBowlerAsync(ball.MatchId, ball.InningNumber, null);

                if (request.OverNumber == 20 && ball.InningNumber == 1)
                {
                    InningState inningstate = await _inningStateService.GetInningState(ball.MatchId, ball.InningNumber) ?? throw new NotFoundException(nameof(InningState));

                    InningStateRequestModel secondInning = new()
                    {
                        MatchId = ball.MatchId,
                        InningNumber = 2,
                        BattingTeamId = inningstate.BowlingTeamId != null ? (int)inningstate.BowlingTeamId : 0, // Inning Change So Swap The Batting And Bowling Team
                        BowlingTeamId = inningstate.BattingTeamId != null ? (int)inningstate.BattingTeamId : 0,
                    };

                    await _inningStateService.AddAsync(secondInning);

                    // await _matchService.ChangeMatchInning(ball.MatchId, 2);

                }

            }
        }
    }

    public async Task<List<BallEvent>> GetBallEventsForMatch(int matchId, int? inningNumber = null)
    {
        if (inningNumber.HasValue)
        {
            return await _ballEventRepository.GetEagerLoadAllWithFilterAsync(b => b.MatchId == matchId && b.InningNumber == inningNumber.Value, b => b.Bowler, b => b.Batsman, b => b.NonStriker);
        }
        else
        {
            return await _ballEventRepository.GetEagerLoadAllWithFilterAsync(b => b.MatchId == matchId, b => b.Bowler, b => b.Batsman, b => b.NonStriker);
        }
    }

    public async Task<List<BallEvent>> GetRecentBalls(int matchId, int inningNumber, int count, bool includeExtras = true)
    {
        var allBalls = await _ballEventRepository.GetAllWithFilterAsync(b => b.MatchId == matchId && b.InningNumber == inningNumber);
        if (!allBalls.Any())
            return new List<BallEvent>();
        // Find the latest over number
        int latestOver = allBalls.Max(b => b.OverNumber);
        // Get all balls from the latest over
        var ballsOfLatestOver = allBalls.Where(b => b.OverNumber == latestOver)
                                        .OrderBy(b => b.BallNumber)
                                        .ThenBy(b => b.Timestamp)
                                        .ToList();
        return ballsOfLatestOver;
    }

    public async Task<List<int>> GetOutPlayersListByMatchId(int matchId)
    {
        var balls = await _ballEventRepository.GetAllWithFilterAsync(b => b.MatchId == matchId && b.WicketType != null);

        var playerIds = balls.Select(b => b.DismissedPlayerId ?? 0).ToList();

        return playerIds;
    }
}
