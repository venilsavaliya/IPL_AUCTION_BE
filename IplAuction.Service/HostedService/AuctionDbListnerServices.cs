using IplAuction.Entities.Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Npgsql;

namespace IplAuction.Service.HostedService;

public class AuctionDbListenerService(IServiceProvider serviceProvider, IOptions<ConnectionStrings> connectionStrigns) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private readonly ConnectionStrings _connectionString = connectionStrigns.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var conn = new NpgsqlConnection(_connectionString.DefaultConnection);
        await conn.OpenAsync(stoppingToken);

        conn.Notification += (sender, e) =>
        {
            Console.WriteLine($"Payload was: {e.Payload}");

            // TODO Use Of SignalR For BroadCasting
        };

        using (var listenCmd = new NpgsqlCommand("LISTEN auction_updates;", conn))
        {
            await listenCmd.ExecuteNonQueryAsync(stoppingToken);
        }

        Console.WriteLine("Listening to PostgreSQL notifications...");

        while (!stoppingToken.IsCancellationRequested)
        {
            await conn.WaitAsync(stoppingToken); // Waits for NOTIFY to arrive
        }
    }
}

