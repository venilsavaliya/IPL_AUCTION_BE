using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.BallEvent;

namespace IplAuction.Service.Interface;

public interface IBallEventService
{
    Task AddBall(AddBallEventRequest request);
    Task<List<BallEvent>> GetBallEventsForMatch(int matchId, int? inningNumber = null);
    Task<List<BallEvent>> GetRecentBalls(int matchId, int inningNumber, int count, bool includeExtras = true);
    Task<List<int>> GetOutPlayersListByMatchId(int matchId);
}
