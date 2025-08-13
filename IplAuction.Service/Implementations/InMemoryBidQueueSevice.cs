using System.Collections.Concurrent;
using IplAuction.Entities.ViewModels.Bid;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

// INFO : THIS SERVICE IS NO LONGER IN USE

public class InMemoryBidQueueSevice : IBidQueueService
{
    private readonly ConcurrentQueue<PlaceBidRequestModel> _queue = new();

    public void Enqueue(PlaceBidRequestModel bid) => _queue.Enqueue(bid);

    public bool TryDequeue(out PlaceBidRequestModel bid) => _queue.TryDequeue(out bid!);
}
