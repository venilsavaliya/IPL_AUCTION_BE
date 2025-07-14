using IplAuction.Entities.ViewModels.BallEvent;

namespace IplAuction.Service.Interface;

public interface IBallEventService
{
    Task AddBall(AddBallEventRequest request);
}
