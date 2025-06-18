using IplAuction.Entities.Enums;
using IplAuction.Entities.Models;
using IplAuction.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IplAuction.Service.HostedService;

public class AuctionStatusUpdater(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("Running background task at: " + DateTime.UtcNow.AddMinutes(2));

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var auctionRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<Auction>>();

                List<Auction> auctions = await auctionRepository.GetAllWithFilterAsync(
                    a => !a.IsDeleted &&
                         a.AuctionStatus == AuctionStatus.Scheduled &&
                         a.StartDate <= DateTime.UtcNow
                );

                foreach (Auction a in auctions)
                {
                    a.AuctionStatus = AuctionStatus.Live;
                }

                if (auctions.Count != 0)
                {
                    await auctionRepository.SaveChangesAsync();
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // runs every 1 minute
        }
    }
}


