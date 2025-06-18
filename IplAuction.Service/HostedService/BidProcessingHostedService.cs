using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IplAuction.Service.HostedService;

public class BidProcessingHostedService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var bidQueue = scope.ServiceProvider.GetRequiredService<IBidQueueService>();

            if (bidQueue.TryDequeue(out var bid))
            {
               
                var bidRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<Bid>>();
                var auctionRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<Auction>>();
                var bidService = scope.ServiceProvider.GetRequiredService<IBidService>();

                Auction? auction = await auctionRepository.GetWithFilterAsync(a => a.Id == bid.AuctionId);

                if (auction == null || auction.CurrentBid == 0 || bid.BidAmount <= auction.CurrentBid)
                    continue;

                await bidService.PlaceBid(bid);
            }

            await Task.Delay(100); // avoid tight loop
        }
    }
}

