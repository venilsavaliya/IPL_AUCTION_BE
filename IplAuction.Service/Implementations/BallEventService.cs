using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.BallEvent;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class BallEventService(IBallEventRepository ballEventRepository) : IBallEventService
{
    private readonly IBallEventRepository _ballEventRepository = ballEventRepository;

    public async Task AddBall(AddBallEventRequest request)
    {
        BallEvent ball = new(request);

        await _ballEventRepository.AddAsync(ball);

        await _ballEventRepository.SaveChangesAsync();
    }
}
